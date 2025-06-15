// Program.cs
// Requires Newtonsoft.Json (Json.NET) package. Install via NuGet: Install-Package Newtonsoft.Json

// .NET 4.7 Console WebSocket 서버 + Newtonsoft.Json을 이용한 JSON 통신 구현

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json; // Json.NET

#region 데이터 모델
public class ClientInfo
{
    public string Uid { get; set; }
    public int MapId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public int State { get; set; }
    public bool IsOccupied { get; set; }
}
public class MonsterInfo
{
    public string Mid { get; set; }
    public int MapId { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public bool IsOccupied { get; set; }
}
public class ItemInfo
{
    public string Iid { get; set; }
    public int MapId { get; set; }
    public string Name { get; set; }
    public int Power { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}
#endregion

#region 전투 세션
public class CombatSession
{
    public string Uid1, Uid2;
    private ConcurrentDictionary<string, Tuple<string, int>> requests = new ConcurrentDictionary<string, Tuple<string, int>>();
    public CombatSession(string u1, string u2) { Uid1 = u1; Uid2 = u2; }
    public async Task AddActionAsync(string uid, string action, int value)
    {
        requests[uid] = Tuple.Create(action, value);
        var opponent = (uid == Uid1 ? Uid2 : Uid1);
        if (requests.ContainsKey(opponent))
        {
            var a = requests[Uid1];
            var b = requests[Uid2];
            int dmg1to2 = (a.Item1 == "attack" ? a.Item2 : 0);
            int dmg2to1 = (b.Item1 == "attack" ? b.Item2 : 0);
            var res1 = new { cmd = (a.Item1 == "attack" ? 208 : 209), target_action = b.Item1, my_damage = dmg2to1, target_damage = dmg1to2 };
            var res2 = new { cmd = (b.Item1 == "attack" ? 208 : 209), target_action = a.Item1, my_damage = dmg1to2, target_damage = dmg2to1 };
            Program.SendToClient(Uid1, res1);
            Program.SendToClient(Uid2, res2);
            Program.RemoveCombatSession(Uid1, Uid2);
        }
    }
}
#endregion

public class Program
{
    #region 전역 스토어
    static readonly ConcurrentDictionary<string, ClientInfo> clients = new ConcurrentDictionary<string, ClientInfo>();
    static readonly ConcurrentDictionary<int, int[,]> maps = new ConcurrentDictionary<int, int[,]>();
    static readonly ConcurrentDictionary<int, ConcurrentDictionary<string, MonsterInfo>> mapMonsters = new ConcurrentDictionary<int, ConcurrentDictionary<string, MonsterInfo>>();
    static readonly ConcurrentDictionary<int, ConcurrentDictionary<string, ItemInfo>> mapItems = new ConcurrentDictionary<int, ConcurrentDictionary<string, ItemInfo>>();
    static readonly ConcurrentDictionary<string, WebSocket> uidToSession = new ConcurrentDictionary<string, WebSocket>();
    static readonly ConcurrentDictionary<WebSocket, string> sessionUids = new ConcurrentDictionary<WebSocket, string>();
    static readonly ConcurrentDictionary<string, CombatSession> combatSessions = new ConcurrentDictionary<string, CombatSession>();
    static int nextUid = 1, nextMid = 1, nextIid = 1;
    #endregion

    public static void Main(string[] args)
    {
        // 백그라운드 서버 실행
        Task.Run(() => RunServerAsync());
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }

    static async Task RunServerAsync()
    {
        Console.WriteLine("Server Started...");
        // 맵 초기화 및 샘플 데이터
        for (int id = 1; id <= 2; id++)
        {
            maps[id] = new int[100, 100];
            mapMonsters[id] = new ConcurrentDictionary<string, MonsterInfo>();
            mapItems[id] = new ConcurrentDictionary<string, ItemInfo>();
        }
        var m1 = new MonsterInfo { Mid = (nextMid++).ToString(), MapId = 1, Name = "Goblin", Level = 5, HP = 100, Attack = 10, X = 10, Y = 5, IsOccupied = false };
        mapMonsters[1][m1.Mid] = m1;
        var item1 = new ItemInfo { Iid = (nextIid++).ToString(), MapId = 1, Name = "Sword", Power = 5, X = 15, Y = 7 };
        mapItems[1][item1.Iid] = item1;

        var listener = new HttpListener();
        listener.Prefixes.Add("http://+:25565/ws/");
        try
        {
            listener.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Listener failed to start: {ex.Message}");
            return;
        }
        Console.WriteLine("WebSocket server listening...");

        while (true)
        {
            var ctx = await listener.GetContextAsync();
            if (ctx.Request.IsWebSocketRequest)
            {
                var wsCtx = await ctx.AcceptWebSocketAsync(null);
                _ = HandleSessionAsync(wsCtx.WebSocket);
            }
            else
            {
                ctx.Response.StatusCode = 400;
                ctx.Response.Close();
            }
        }
    }

    static async Task HandleSessionAsync(WebSocket ws)
    {
        // 클라이언트 세션 시작 로그
        Console.WriteLine($"[LOG] Client connected");
        sessionUids[ws] = null;
        var buf = new byte[4096];
        while (ws.State == WebSocketState.Open)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buf), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine($"[LOG] Client disconnected: {sessionUids[ws]}");
                break;
            }
            var msg = Encoding.UTF8.GetString(buf, 0, result.Count);
            // 수신 메시지 로그
            Console.WriteLine($"[LOG] Received from {sessionUids[ws] ?? "unknown"}: {msg}");
            var req = JsonConvert.DeserializeObject<Dictionary<string, object>>(msg);
            int cmd = Convert.ToInt32(req["cmd"]);
            await DispatchAsync(ws, cmd, req);
        }
    }

    static async Task DispatchAsync(WebSocket ws, int cmd, Dictionary<string, object> req)
    {
        switch (cmd)
        {
            case 100: await HandleConnectAsync(ws, req); break;
            case 200: await HandleMoveAsync(ws, req); break;
            case 102: await HandlePositionAsync(ws, req); break;
            case 103: await HandleAllAsync(ws, req); break;
            case 104: await HandleRemoveAsync(ws, req); break;
            case 105: await HandleMonsterInfoAsync(ws, req); break;
            case 106: await HandlePVPAsync(ws, req); break;
            case 107: await HandleMonsterBattleAsync(ws, req); break;
            case 108: await HandleAttackAsync(ws, req); break;
            case 109: await HandleDefendAsync(ws, req); break;
            case 110: await HandlePickupAsync(ws, req); break;
            case 111: await HandleItemInfoAsync(ws, req); break;
            case 112: await HandleUserUpdateAsync(ws, req); break;
            case 113: await HandleUpgradeAsync(ws, req); break;
        }
    }

    #region 핸들러 구현
    static async Task HandleConnectAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string uid = req.ContainsKey("uid") ? req["uid"].ToString() : string.Empty;
        if (string.IsNullOrEmpty(uid))
        {
            uid = (nextUid++).ToString();
            clients[uid] = new ClientInfo { Uid = uid, MapId = 1, X = 0, Y = 0, State = 0, IsOccupied = false };
        }
        else if (!clients.ContainsKey(uid))
        {
            await SendAsync(ws, new { status = 400, message = "정확하지 않은 uid" });
            return;
        }
        sessionUids[ws] = uid;
        uidToSession[uid] = ws;
        await SendAsync(ws, new { status = 200, message = "connected", uid = uid });
    }

    static async Task HandleMoveAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string uid = req["uid"].ToString();
        int mapId = Convert.ToInt32(req["map_id"]);
        double dx = Convert.ToDouble(req["dx"]), dy = Convert.ToDouble(req["dy"]);
        if (!maps.ContainsKey(mapId))
        {
            await SendAsync(ws, new { status = 401, message = "옳바르지 않은 map id" });
            return;
        }
        var ci = clients[uid];
        double nx = ci.X + dx, ny = ci.Y + dy;
        var grid = maps[mapId];
        int mx = (int)nx, my = (int)ny;
        if (mx < 0 || my < 0 || mx >= grid.GetLength(0) || my >= grid.GetLength(1) || grid[mx, my] != 0)
        {
            await SendAsync(ws, new { status = 401, message = "Blocked area" });
            return;
        }
        if (ci.IsOccupied)
        {
            await SendAsync(ws, new { status = 401, message = "Currently occupied" });
            return;
        }
        ci.X = nx; ci.Y = ny; ci.MapId = mapId; clients[uid] = ci;
        await SendAsync(ws, new { status = 201, map_id = mapId, dx = nx, dy = ny });
    }

    static async Task HandlePositionAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string uid = req["uid"].ToString();
        if (!clients.ContainsKey(uid))
        {
            await SendAsync(ws, new { status = 400, message = "존재하지 않는 uid" });
            return;
        }
        var ci = clients[uid];
        await SendAsync(ws, new { status = 202, uid = uid, state = ci.State, x = ci.X, y = ci.Y });
    }

    static async Task HandleAllAsync(WebSocket ws, Dictionary<string, object> req)
    {
        int mapId = Convert.ToInt32(req["map_id"]);
        if (!maps.ContainsKey(mapId))
        {
            await SendAsync(ws, new { status = 403, message = "존재하지 않는 맵" });
            return;
        }
        var uls = new List<object>();
        foreach (var kv in clients) if (kv.Value.MapId == mapId) uls.Add(new { uid = kv.Key, x = kv.Value.X, y = kv.Value.Y });
        var mls = new List<object>();
        foreach (var kv in mapMonsters[mapId]) mls.Add(new { mid = kv.Key, x = kv.Value.X, y = kv.Value.Y });
        var ils = new List<object>();
        foreach (var kv in mapItems[mapId]) ils.Add(new { iid = kv.Key, x = kv.Value.X, y = kv.Value.Y });
        var body = new { uid_list = uls, mid_list = mls, item_list = ils };
        await SendAsync(ws, new { status = 203, body = body });
    }

    static async Task HandleRemoveAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string uid = req["uid"].ToString();
        if (!clients.TryRemove(uid, out _))
        {
            await SendAsync(ws, new { status = 404, message = "존재하지 않는 uid" });
            return;
        }
        await BroadcastAsync(new { status = 204, uid = uid });
    }

    static async Task HandleMonsterInfoAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string mid = req["mid"].ToString();
        int mapId = Convert.ToInt32(req["map_id"]);
        if (!mapMonsters.ContainsKey(mapId) || !mapMonsters[mapId].TryGetValue(mid, out var mi))
        {
            await SendAsync(ws, new { status = 405, message = "존재하지 않는 mid" });
            return;
        }
        await SendAsync(ws, new { status = 205, monster_name = mi.Name, monster_level = mi.Level, monster_hp = mi.HP, monster_attack = mi.Attack });
    }

    static async Task HandlePVPAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string u1 = req["uid1"].ToString(), u2 = req["uid2"].ToString();
        if (!clients.ContainsKey(u1) || !clients.ContainsKey(u2))
        {
            await SendAsync(ws, new { cmd = 406, message = "잘못된 uid 혹은 대상없음" });
            return;
        }
        var pair = new[] { u1, u2 }; Array.Sort(pair); var key = $"{pair[0]}:{pair[1]}";
        combatSessions[key] = new CombatSession(pair[0], pair[1]);
        await SendAsync(ws, new { cmd = 206, uid1 = u1, uid2 = u2 });
    }

    static async Task HandleMonsterBattleAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string uid = req["uid"].ToString(), mid = req["mid"].ToString();
        int mapId = Convert.ToInt32(req["map_id"]);
        if (!clients.ContainsKey(uid) || !mapMonsters.ContainsKey(mapId) || !mapMonsters[mapId].ContainsKey(mid) || mapMonsters[mapId][mid].IsOccupied)
        {
            await SendAsync(ws, new { status = 407, message = "잘못된 mid 혹은 점유상태" });
            return;
        }
        await SendAsync(ws, new { status = 207, uid = uid, mid = mid });
    }

    static async Task HandleAttackAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string atk = sessionUids[ws], tgt = req["target_uid"].ToString();
        int dmg = Convert.ToInt32(req["damage"]), mod = Convert.ToInt32(req["modifier"]);
        var pair = new[] { atk, tgt }; Array.Sort(pair); var key = $"{pair[0]}:{pair[1]}";
        if (combatSessions.TryGetValue(key, out var session))
            await session.AddActionAsync(atk, "attack", dmg - mod);
        else
            await SendAsync(ws, new { cmd = 408, message = "No active combat session" });
    }

    static async Task HandleDefendAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string def = sessionUids[ws], tgt = req["target_uid"].ToString();
        int mod = Convert.ToInt32(req["modifier"]);
        var pair = new[] { def, tgt }; Array.Sort(pair); var key = $"{pair[0]}:{pair[1]}";
        if (combatSessions.TryGetValue(key, out var session))
            await session.AddActionAsync(def, "defense", mod);
        else
            await SendAsync(ws, new { cmd = 409, message = "No active combat session" });
    }

    static async Task HandlePickupAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string iid = req["iid"].ToString(); int mapId = Convert.ToInt32(req["map_id"]);
        if (!mapItems.ContainsKey(mapId) || !mapItems[mapId].ContainsKey(iid))
        {
            await SendAsync(ws, new { cmd = 410, message = "존재하지 않는 iid" });
            return;
        }
        await SendAsync(ws, new { status = 210 });
    }

    static async Task HandleItemInfoAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string iid = req["iid"].ToString(); int mapId = Convert.ToInt32(req["map_id"]);
        if (!mapItems.ContainsKey(mapId) || !mapItems[mapId].ContainsKey(iid))
        {
            await SendAsync(ws, new { cmd = 411, message = "존재하지 않는 iid" });
            return;
        }
        var ii = mapItems[mapId][iid];
        await SendAsync(ws, new { cmd = 211, weapon_id = iid, weapon_damage = ii.Power });
    }

    static async Task HandleUserUpdateAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string uid = req["uid"].ToString(); int lvl = Convert.ToInt32(req["level"]);
        if (!clients.ContainsKey(uid))
        {
            await SendAsync(ws, new { cmd = 412, message = "존재하지 않는 uid" });
            return;
        }
        await SendAsync(ws, new { cmd = 212 });
    }

    static async Task HandleUpgradeAsync(WebSocket ws, Dictionary<string, object> req)
    {
        string iid = req["iid"].ToString(); int dmg = Convert.ToInt32(req["weapon_damage"]);
        int mapId = Convert.ToInt32(req["map_id"]);
        if (!mapItems.ContainsKey(mapId) || !mapItems[mapId].ContainsKey(iid))
        {
            await SendAsync(ws, new { cmd = 413, message = "존재하지 않는 iid" });
            return;
        }
        await SendAsync(ws, new { cmd = 213 });
    }
    #endregion

    #region 통신 유틸
    public static async Task SendAsync(WebSocket ws, object obj)
    {
        var msg = JsonConvert.SerializeObject(obj);
        var buf = Encoding.UTF8.GetBytes(msg);
        await ws.SendAsync(new ArraySegment<byte>(buf), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public static void SendToClient(string uid, object obj)
    {
        if (uidToSession.TryGetValue(uid, out var ws) && ws.State == WebSocketState.Open)
        {
            var msg = JsonConvert.SerializeObject(obj);
            var buf = Encoding.UTF8.GetBytes(msg);
            ws.SendAsync(new ArraySegment<byte>(buf), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    public static void RemoveCombatSession(string u1, string u2)
    {
        var pair = new[] { u1, u2 }; Array.Sort(pair);
        var key = $"{pair[0]}:{pair[1]}";
        combatSessions.TryRemove(key, out _);
    }

    // 서버에 연결된 모든 클라이언트에 브로드캐스트
    public static async Task BroadcastAsync(object obj)
    {
        var msg = JsonConvert.SerializeObject(obj);
        var buf = Encoding.UTF8.GetBytes(msg);
        foreach (var ws in uidToSession.Values)
        {
            if (ws != null && ws.State == WebSocketState.Open)
                await ws.SendAsync(new ArraySegment<byte>(buf), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
    #endregion
}
