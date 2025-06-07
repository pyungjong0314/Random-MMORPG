// Program.cs
// .NET 7.0 Console WebSocket 서버 + System.Text.Json 기반 JSON 통신 구현 (API 명세 & 맵별 몬스터/아이템 반영)

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

// 클라이언트 정보 레코드: UID, 차원(map_id), 좌표(X,Y), 상태(state), 점유 여부(IsOccupied)
record ClientInfo(string Uid, int MapId, double X, double Y, int State, bool IsOccupied);

// 몬스터 정보 레코드: MID, MapId, 이름(Name), 레벨(Level), 체력(HP), 공격력(Attack), 위치(X,Y), 점유 여부
record MonsterInfo(string Mid, int MapId, string Name, int Level, int HP, int Attack, double X, double Y, bool IsOccupied);

// 아이템 정보 레코드: IID, MapId, 이름(Name), 위력(Power), 위치(X,Y)
record ItemInfo(string Iid, int MapId, string Name, int Power, double X, double Y);

class Program
{
    // 전역 변수
    static readonly ConcurrentDictionary<string, ClientInfo> clients = new();            // UID -> ClientInfo
    // MapId -> (MID -> MonsterInfo)
    static readonly ConcurrentDictionary<int, ConcurrentDictionary<string, MonsterInfo>> mapMonsters = new();
    // MapId -> (IID -> ItemInfo)
    static readonly ConcurrentDictionary<int, ConcurrentDictionary<string, ItemInfo>> mapItems = new();
    static readonly ConcurrentDictionary<WebSocket, string> sessionUids = new();         // WebSocket -> UID
    static readonly ConcurrentDictionary<WebSocket, object> sessions = new();            // 활성 WebSocket 세션
    static int nextUid = 1;                                                              // UID 생성기 (숫자 문자열)
    static int nextMid = 1;                                                              // MID 생성기
    static int nextIid = 1;                                                              // IID 생성기
    // MapId -> 2D 배열 맵 행렬 (0: 통과 가능, 1: 벽)
    static readonly ConcurrentDictionary<int, int[,]> maps = new();

    public static async Task Main(string[] args)
    {
        // 1) 맵별 초기화 예시: MapId 1,2 생성
        for (int mapId = 1; mapId <= 2; mapId++)
        {
            var grid = new int[100, 100];
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                    grid[i, j] = 0; // 모두 통과 가능
            maps[mapId] = grid;
            mapMonsters[mapId] = new ConcurrentDictionary<string, MonsterInfo>();
            mapItems[mapId] = new ConcurrentDictionary<string, ItemInfo>();
        }

        // 2) 몬스터 및 아이템 맵별 초기 샘플 데이터
        var m1 = new MonsterInfo((nextMid++).ToString(), 1, "Goblin", 5, 100, 10, 10.0, 5.0, false);
        mapMonsters[1][m1.Mid] = m1;
        var m2 = new MonsterInfo((nextMid++).ToString(), 2, "Skeleton", 3, 80, 8, 20.0, 15.0, false);
        mapMonsters[2][m2.Mid] = m2;
        var item1 = new ItemInfo((nextIid++).ToString(), 1, "Sword", 5, 15.0, 7.0);
        mapItems[1][item1.Iid] = item1;
        var item2 = new ItemInfo((nextIid++).ToString(), 2, "Shield", 3, 25.0, 20.0);
        mapItems[2][item2.Iid] = item2;

        // 3) HTTP 리스너 설정 및 WebSocket 경로 등록
        var http = new HttpListener();
        http.Prefixes.Add("http://0.0.0.0:9002/ws/");
        http.Start();
        Console.WriteLine("WebSocket 서버 대기 중... http://0.0.0.0:9002/ws/");

        // 4) 클라이언트 연결 수락 루프
        while (true)
        {
            var context = await http.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                var wsContext = await context.AcceptWebSocketAsync(null);
                _ = HandleSessionAsync(wsContext);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    static async Task HandleSessionAsync(WebSocketContext wsContext)
    {
        var ws = wsContext.WebSocket;
        sessions[ws] = null;
        Console.WriteLine("클라이언트 연결됨");

        var buffer = new byte[4096];
        try
        {
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                try
                {
                    using var doc = JsonDocument.Parse(msg);
                    var root = doc.RootElement;
                    int cmd = root.GetProperty("cmd").GetInt32();
                    await DispatchAsync(ws, cmd, root);
                }
                catch (JsonException)
                {
                    await SendErrorAsync(ws, 0, "Invalid JSON format");
                }
            }
        }
        finally
        {
            sessions.TryRemove(ws, out _);
            sessionUids.TryRemove(ws, out _);
            Console.WriteLine("클라이언트 연결 종료");
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }
    }

    static async Task DispatchAsync(WebSocket ws, int cmd, JsonElement req)
    {
        switch (cmd)
        {
            case 100: await HandleConnect(ws, req); break;
            case 200: await HandleMove(ws, req); break;
            case 102: await HandlePositionRequest(ws, req); break;
            case 103: await HandleAllPositions(ws, req); break;
            case 104: await HandleRemoveObject(ws, req); break;
            case 105: await HandleMonsterInfo(ws, req); break;
            case 106: await HandlePVPRequest(ws, req); break;
            case 107: await HandleMonsterBattleRequest(ws, req); break;
            case 108: await HandleAttack(ws, req); break;
            case 109: await HandleDefend(ws, req); break;
            case 110: await HandlePickupItem(ws, req); break;
            case 111: await HandleItemInfo(ws, req); break;
            case 112: await HandleUserUpdate(ws, req); break;
            case 113: await HandleUpgradeItem(ws, req); break;
            default:
                await SendErrorAsync(ws, 400, "Unknown command");
                break;
        }
    }

    // 100: 서버 연결
    static async Task HandleConnect(WebSocket ws, JsonElement req)
    {
        // 요청에 "uid" 프로퍼티가 있는지 확인
        string uid = string.Empty;
        if (req.TryGetProperty("uid", out var uElem) && uElem.ValueKind == JsonValueKind.String)
        {
            uid = uElem.GetString();
        }

        // 1) uid가 없거나 빈 문자열이면 신규 연결
        if (string.IsNullOrEmpty(uid))
        {
            var newUid = (nextUid++).ToString();
            clients[newUid] = new ClientInfo(newUid, 1, 0, 0, 0, false);
            sessionUids[ws] = newUid;
            var res = new { status = 200, message = "connected", uid = newUid };
            await SendJsonAsync(ws, res);
            return;
        }

        // 2) uid가 제공되었지만 등록된 클라이언트가 아닌 경우
        if (!clients.ContainsKey(uid))
        {
            var err = new { status = 400, message = "정확하지 않은 uid" };
            await SendJsonAsync(ws, err);
            return;
        }

        // 3) 유효한 uid인 경우 (재접속)
        sessionUids[ws] = uid;
        var resReconnect = new { status = 200, message = "connected", uid = uid };
        await SendJsonAsync(ws, resReconnect);
    }

    // 200: 캐릭터 이동 (MapId, dx, dy: 현재 좌표 기준 상대 이동, 맵 경계 및 벽 체크)
    static async Task HandleMove(WebSocket ws, JsonElement req)
    {
        string uid = req.GetProperty("uid").GetString();
        int mapId = req.GetProperty("map_id").GetInt32();
        double dx = req.GetProperty("dx").GetDouble(); // 현재 위치 기준 이동량 x
        double dy = req.GetProperty("dy").GetDouble(); // 현재 위치 기준 이동량 y

        if (!maps.ContainsKey(mapId))
        {
            var err = new { status = 401, message = "옳바르지 않은 map id" };
            await SendJsonAsync(ws, err);
            return;
        }
        if (!clients.TryGetValue(uid, out var ci))
        {
            var err = new { status = 400, message = "존재하지 않는 uid" };
            await SendJsonAsync(ws, err);
            return;
        }
        // 이동하려는 새로운 좌표 계산
        double newX = ci.X + dx;
        double newY = ci.Y + dy;
        var grid = maps[mapId];
        int mx = (int)newX;
        int my = (int)newY;
        // 맵 범위 및 장애물 체크
        if (mx < 0 || my < 0 || mx >= grid.GetLength(0) || my >= grid.GetLength(1) || grid[mx, my] != 0)
        {
            var err = new { status = 401, message = "Blocked area" };
            await SendJsonAsync(ws, err);
            return;
        }
        // 점유 상태 체크
        if (ci.IsOccupied)
        {
            var err = new { status = 401, message = "Currently occupied" };
            await SendJsonAsync(ws, err);
            return;
        }
        // 위치 업데이트
        clients[uid] = ci with { X = newX, Y = newY, MapId = mapId };
        var res = new { status = 201, map_id = mapId, dx = newX, dy = newY };
        await SendJsonAsync(ws, res);
    }

    // 102: 객체 위치 요청
    static async Task HandlePositionRequest(WebSocket ws, JsonElement req)
    {
        string uid = req.GetProperty("uid").GetString();
        if (!clients.TryGetValue(uid, out var ci))
        {
            var err = new { status = 400, message = "존재하지 않는 uid" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new { status = 202, uid = uid, state = ci.State.ToString(), x = ci.X, y = ci.Y };
        await SendJsonAsync(ws, res);
    }

    // 103: 전체 객체 위치 요청
    static async Task HandleAllPositions(WebSocket ws, JsonElement req)
    {
        int mapId = req.GetProperty("map_id").GetInt32();
        if (!maps.ContainsKey(mapId))
        {
            var err = new { status = 403, message = "존재하지 않는 맵" };
            await SendJsonAsync(ws, err);
            return;
        }
        var uidList = new List<object>();
        foreach (var kv in clients)
        {
            if (kv.Value.MapId == mapId)
                uidList.Add(new { uid = kv.Key, x = kv.Value.X, y = kv.Value.Y });
        }
        var midList = new List<object>();
        foreach (var kv in mapMonsters[mapId])
        {
            midList.Add(new { mid = kv.Key, x = kv.Value.X, y = kv.Value.Y });
        }
        var itemList = new List<object>();
        foreach (var kv in mapItems[mapId])
        {
            itemList.Add(new { iid = kv.Key, x = kv.Value.X, y = kv.Value.Y });
        }
        var body = new { uid_list = uidList, mid_list = midList, item_list = itemList };
        var res = new { status = 203, body = body };
        await SendJsonAsync(ws, res);
    }

    // 104: 객체 제거
    static async Task HandleRemoveObject(WebSocket ws, JsonElement req)
    {
        string uid = req.GetProperty("uid").GetString();
        if (!clients.TryRemove(uid, out _))
        {
            var err = new { status = 404, message = "존재하지 않는 uid" };
            await SendJsonAsync(ws, err);
            return;
        }
        var broad = new { status = 204, uid = uid }; // Broadcast
        await BroadcastAsync(broad);
    }

    // 105: 몬스터 정보 확인
    static async Task HandleMonsterInfo(WebSocket ws, JsonElement req)
    {
        string mid = req.GetProperty("mid").GetString();
        int mapId = req.GetProperty("map_id").GetInt32();
        if (!mapMonsters.ContainsKey(mapId) || !mapMonsters[mapId].TryGetValue(mid, out var mi))
        {
            var err = new { status = 405, message = "존재하지 않는 mid" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new
        {
            status = 205,
            monster_name = mi.Name,
            monster_level = mi.Level,
            monster_hp = mi.HP,
            monster_attack = mi.Attack
        };
        await SendJsonAsync(ws, res);
    }

    // 106: PVP 전투 돌입 요청
    static async Task HandlePVPRequest(WebSocket ws, JsonElement req)
    {
        string uid1 = req.GetProperty("uid1").GetString();
        string uid2 = req.GetProperty("uid2").GetString();
        if (!clients.ContainsKey(uid1) || !clients.ContainsKey(uid2))
        {
            var err = new { cmd = 406, message = "잘못된 uid 혹은 대상없음" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new { cmd = 206, uid1 = uid1, uid2 = uid2 };
        await SendJsonAsync(ws, res);
    }

    // 107: 몬스터 전투 돌입 요청
    static async Task HandleMonsterBattleRequest(WebSocket ws, JsonElement req)
    {
        string uid = req.GetProperty("uid").GetString();
        string mid = req.GetProperty("mid").GetString();
        int mapId = req.GetProperty("map_id").GetInt32();
        if (!clients.ContainsKey(uid) || !mapMonsters.ContainsKey(mapId) || !mapMonsters[mapId].TryGetValue(mid, out var mi) || mi.IsOccupied)
        {
            var err = new { status = 407, message = "잘못된 mid 혹은 점유상태" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new { status = 207, uid = uid, mid = mid };
        await SendJsonAsync(ws, res);
    }

        // 전투 상태 저장: (uid1, uid2) 쌍으로 전투 중인지, 각자의 요청을 저장
    static readonly ConcurrentDictionary<string, (string Action, int Modifier)> combatRequests = new();
    
    // 108: 공격 요청
    static async Task HandleAttack(WebSocket ws, JsonElement req)
    {
        string attacker = sessionUids[ws];
        string target = req.GetProperty("target_uid").GetString();
        int damage = req.GetProperty("damage").GetInt32();
        int modifier = req.GetProperty("modifier").GetInt32();
        
        // 전투 상대 간 쌍 키 생성 (작은 문자 순으로 정렬하여 일관성 유지)
        var pair = new[] { attacker, target };
        Array.Sort(pair);
        string combatKey = pair[0] + ":" + pair[1];
        
        // 자신 요청 저장
        combatRequests[$"{combatKey}:{attacker}"] = ("attack", damage - modifier);
        
        // 상대 요청이 들어왔는지 확인
        if (combatRequests.TryGetValue($"{combatKey}:{target}", out var oppReq))
        {
            // 양쪽 요청 존재 → 결과 계산 및 전송
            var myReq = combatRequests[$"{combatKey}:{attacker}"];
            // myReq.Modifier 필드는 이미 damage-modifier 로 전달됨
            int myDamageToTarget = myReq.Modifier;
            int oppDamageToMe = oppReq.Modifier;

            var resToAttacker = new { cmd = 208, target_action = oppReq.Action, my_damage = oppDamageToMe, target_damage = myDamageToTarget };
            var resToTarget = new { cmd = 208, target_action = myReq.Action, my_damage = myDamageToTarget, target_damage = oppDamageToMe };
            
            // 해당 두 세션 찾기
            foreach (var kv in sessionUids)
            {
                if (kv.Value == attacker)
                    await SendJsonAsync(kv.Key, resToAttacker);
                else if (kv.Value == target)
                    await SendJsonAsync(kv.Key, resToTarget);
            }
            
            // 완료 후 요청 제거
            combatRequests.TryRemove($"{combatKey}:{attacker}", out _);
            combatRequests.TryRemove($"{combatKey}:{target}", out _);
        }
        else
        {
            // 상대 요청 대기: 응답 없음
        }
    }

    // 109: 방어 요청
    static async Task HandleDefend(WebSocket ws, JsonElement req)
    {
        string defender = sessionUids[ws];
        string target = req.GetProperty("target_uid").GetString();
        int modifier = req.GetProperty("modifier").GetInt32();
        
        // 전투 상대 간 쌍 키 생성
        var pair = new[] { defender, target };
        Array.Sort(pair);
        string combatKey = pair[0] + ":" + pair[1];
        
        // 자신 요청 저장 (방어)
        combatRequests[$"{combatKey}:{defender}"] = ("defense", modifier);
        
        // 상대 요청이 들어왔는지 확인
        if (combatRequests.TryGetValue($"{combatKey}:{target}", out var oppReq))
        {
            // 양쪽 요청 존재 → 결과 계산 및 전송
            var myReq = combatRequests[$"{combatKey}:{defender}"];
            int myDamageToTarget = 0; // 방어 시 피해 없음
            int oppDamageToMe = oppReq.Modifier;

            var resToDefender = new { cmd = 209, target_action = oppReq.Action, my_damage = oppDamageToMe, target_damage = myDamageToTarget };
            var resToTarget = new { cmd = 209, target_action = myReq.Action, my_damage = myDamageToTarget, target_damage = oppDamageToMe };
            
            // 해당 두 세션 찾기
            foreach (var kv in sessionUids)
            {
                if (kv.Value == defender)
                    await SendJsonAsync(kv.Key, resToDefender);
                else if (kv.Value == target)
                    await SendJsonAsync(kv.Key, resToTarget);
            }
            
            // 완료 후 요청 제거
            combatRequests.TryRemove($"{combatKey}:{defender}", out _);
            combatRequests.TryRemove($"{combatKey}:{target}", out _);
        }
        else
        {
            // 상대 요청 대기: 응답 없음
        }
    }        var res = new { cmd = 208, target_action = "defense", my_damage = 0, target_damage = damage - modifier };
        await SendJsonAsync(ws, res);
    }

    // 109: 방어 요청
    static async Task HandleDefend(WebSocket ws, JsonElement req)
    {
        int modifier = req.GetProperty("modifier").GetInt32();
        string targetUid = req.GetProperty("target_uid").GetString();
        if (!clients.ContainsKey(targetUid))
        {
            var err = new { cmd = 409, message = "exception occured" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new { cmd = 209, target_action = "defense", my_damage = 0, target_damage = modifier };
        await SendJsonAsync(ws, res);
    }

    // 110: 아이템 획득 신청
    static async Task HandlePickupItem(WebSocket ws, JsonElement req)
    {
        string iid = req.GetProperty("iid").GetString();
        int mapId = req.GetProperty("map_id").GetInt32();
        if (!mapItems.ContainsKey(mapId) || !mapItems[mapId].ContainsKey(iid))
        {
            var err = new { cmd = 410, message = "존재하지 않는 iid" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new { status = 210 };
        await SendJsonAsync(ws, res);
    }

    // 111: 아이템 정보 요청
    static async Task HandleItemInfo(WebSocket ws, JsonElement req)
    {
        string iid = req.GetProperty("iid").GetString();
        int mapId = req.GetProperty("map_id").GetInt32();
        if (!mapItems.ContainsKey(mapId) || !mapItems[mapId].ContainsKey(iid))
        {
            var err = new { cmd = 411, message = "존재하지 않는 iid" };
            await SendJsonAsync(ws, err);
            return;
        }
        var ii = mapItems[mapId][iid];
        var res = new { cmd = 211, weapon_id = iid, weapon_damage = ii.Power };
        await SendJsonAsync(ws, res);
    }

    // 112: 사용자 정보 수정 요청
    static async Task HandleUserUpdate(WebSocket ws, JsonElement req)
    {
        string uid = req.GetProperty("uid").GetString();
        int level = req.GetProperty("level").GetInt32();
        if (!clients.ContainsKey(uid))
        {
            var err = new { cmd = 412, message = "존재하지 않는 uid" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new { cmd = 212 };
        await SendJsonAsync(ws, res);
    }

    // 113: 아이템 강화 요청
    static async Task HandleUpgradeItem(WebSocket ws, JsonElement req)
    {
        string iid = req.GetProperty("iid").GetString();
        int weaponDamage = req.GetProperty("weapon_damage").GetInt32();
        if (!items.ContainsKey(iid))
        {
            var err = new { cmd = 413, message = "존재하지 않는 iid" };
            await SendJsonAsync(ws, err);
            return;
        }
        var res = new { cmd = 213 };
        await SendJsonAsync(ws, res);
    }

    static async Task SendJsonAsync(WebSocket ws, object obj)
    {
        string msg = JsonSerializer.Serialize(obj);
        byte[] buffer = Encoding.UTF8.GetBytes(msg);
        await ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
    }

    static async Task SendErrorAsync(WebSocket ws, int status, string message)
    {
        var err = new { status = status, message = message };
        await SendJsonAsync(ws, err);
    }

    static async Task BroadcastAsync(object obj)
    {
        string msg = JsonSerializer.Serialize(obj);
        byte[] buffer = Encoding.UTF8.GetBytes(msg);
        foreach (var ws in sessions.Keys)
        {
            if (ws.State == WebSocketState.Open)
                await ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
