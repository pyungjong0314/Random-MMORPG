/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Server
{
    // Program.cs
    // Requires Newtonsoft.Json (Json.NET) package. Install via NuGet: Install-Package Newtonsoft.Json

    // .NET 4.7 Console WebSocket 서버 + Newtonsoft.Json을 이용한 JSON 통신 구현

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json; // Json.NET

    #region 데이터 모델
    public class ClientInfo
    {
        // 기존 식별 및 위치 정보
        public string Uid { get; set; }

        // 캐릭터 고유 속성
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int CharacterLevel { get; set; }
        public int CharacterExp { get; set; }
        public int CharacterMoney { get; set; }
        public int CharacterMapId { get; set; }
        public (int x, int y) CharacterLocation { get; set; }
        public int CharacterHp { get; set; }
        public int CharacterAttack { get; set; }

        // 장비
        public Weapon CharacterSword { get; set; }
        public Weapon CharacterShield { get; set; }
        public List<Weapon> CharacterWeapons { get; set; } = new List<Weapon>();

        // 상태 제어
        public int State { get; set; }
        public bool IsOccupied { get; set; }
    }

    // 최소한의 Weapon 클래스 정의
    public class Weapon
    {
        public string Name { get; set; }
        public int Damage { get; set; }
    }

    public class MonsterInfo
    {
        // 기존 식별 및 위치 정보
        public string Mid { get; set; }
        public int MapId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsOccupied { get; set; }

        // 일반몹 속성
        public string MonsterName { get; set; }
        public string MonsterId { get; set; }        // == Mid
        public int MonsterLevel { get; set; }
        public int MonsterCoinValue { get; set; } = 0;
        public int MonsterMapId { get; set; }
        public (int x, int y) MonsterLocation { get; set; }
        public int MonsterHp { get; set; }
        public int MonsterAttackAbility { get; set; }
        public int MonsterDefenseAbility { get; set; }
        public int MonsterExperience { get; set; }
    }

    // 아이템은 그대로 사용
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
        private readonly ConcurrentDictionary<string, Tuple<string, int>> requests = new ConcurrentDictionary<string, Tuple<string, int>>();

        public CombatSession(string u1, string u2)
        {
            Uid1 = u1; Uid2 = u2;
        }

        public async Task AddActionAsync(string uid, string action, int value)
        {
            // 공격 또는 방어 요청 저장
            requests[uid] = Tuple.Create(action, value);
            var opponent = uid == Uid1 ? Uid2 : Uid1;

            // 두 쪽 모두 요청을 냈으면 결과 계산
            if (requests.ContainsKey(opponent))
            {
                var a = requests[Uid1];
                var b = requests[Uid2];
                int dmg1to2 = a.Item1 == "attack" ? a.Item2 : 0;
                int dmg2to1 = b.Item1 == "attack" ? b.Item2 : 0;

                // 상태 코드 508 = 공격 응답, 509 = 방어 응답
                var res1 = new { status = a.Item1 == "attack" ? 508 : 509, target_action = b.Item1, my_damage = dmg2to1, target_damage = dmg1to2 };
                var res2 = new { status = b.Item1 == "attack" ? 508 : 509, target_action = a.Item1, my_damage = dmg1to2, target_damage = dmg2to1 };

                Program.SendToClient(Uid1, res1);
                Program.SendToClient(Uid2, res2);


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

            // 샘플 몬스터 생성
            var m1 = new MonsterInfo
            {
                Mid = (nextMid++).ToString(),
                MonsterId = null,                // will be set below
                MapId = 1,
                MonsterMapId = 1,
                MonsterName = "Goblin",
                MonsterLevel = 5,
                MonsterCoinValue = 10,
                MonsterLocation = (10, 5),
                X = 10,
                Y = 5,
                MonsterHp = 100,
                MonsterAttackAbility = 10,
                MonsterDefenseAbility = 2,
                MonsterExperience = 20,
                IsOccupied = false
            };
            m1.MonsterId = m1.Mid;  // 통일
            mapMonsters[1][m1.Mid] = m1;

            // 샘플 아이템
            var item1 = new ItemInfo
            {
                Iid = (nextIid++).ToString(),
                MapId = 1,
                Name = "Sword",
                Power = 5,
                X = 15,
                Y = 7
            };
            mapItems[1][item1.Iid] = item1;

            // (필요하다면) 샘플 클라이언트 미리 생성
            var c1 = new ClientInfo
            {
                Uid = (nextUid++).ToString(),
                CharacterId = 1,
                CharacterName = "Hero",
                CharacterLevel = 1,
                CharacterExp = 0,
                CharacterMoney = 100,
                CharacterMapId = 1,
                CharacterLocation = (0, 0),
                CharacterHp = 50,
                CharacterAttack = 8,
                CharacterSword = new Weapon { Name = "Basic Sword", Damage = 5 },
                CharacterShield = new Weapon { Name = "Wooden Shield", Damage = 1 },
                State = 0,
                IsOccupied = false
            };
            clients[c1.Uid] = c1;

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
                case 101: await HandleMoveAsync(ws, req); break;
                case 102: await HandlePositionAsync(ws, req); break;
                case 103: await HandleAllAsync(ws, req); break;
                case 104: await HandleRemoveAsync(ws, req); break;
                case 105: await HandleMonsterInfoAsync(ws, req); break;

                case 107: await HandleMonsterBattleAsync(ws, req); break;
                case 108: await HandleAttackAsync(ws, req); break;
                case 109: await HandleDefendAsync(ws, req); break;
                case 110: await HandlePickupAsync(ws, req); break;
                case 111: await HandleItemInfoAsync(ws, req); break;
                case 112: await HandleUserUpdateAsync(ws, req); break;
                case 106: await HandlePVPRequestAsync(ws, req); break;
                case 115: await HandlePVPAcceptAsync(ws, req); break;
            }
        }

        #region 핸들러 구현
        static async Task HandleConnectAsync(WebSocket ws, Dictionary<string, object> req)
        {
            string uid = req.ContainsKey("uid") ? req["uid"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(uid))
            {
                uid = (nextUid++).ToString();
                clients[uid] = new ClientInfo
                {
                    Uid = uid,
                    CharacterId = int.Parse(uid),         // uid와 동일
                    CharacterName = "NewPlayer",
                    CharacterLevel = 1,
                    CharacterExp = 0,
                    CharacterMoney = 100,
                    CharacterMapId = 1,
                    CharacterLocation = (0, 0),
                    CharacterHp = 100,
                    CharacterAttack = 10,
                    CharacterSword = new Weapon { Name = "Starter Sword", Damage = 5 },
                    CharacterShield = new Weapon { Name = "Starter Shield", Damage = 2 },
                    State = 0,
                    IsOccupied = false
                };
            }
            else if (!clients.ContainsKey(uid))
            {
                await SendAsync(ws, new { status = 400, message = "Invalid uid" });
                return;
            }
            sessionUids[ws] = uid;
            uidToSession[uid] = ws;
            var ci = clients[uid];
            await SendAsync(ws, new
            {
                status = 200,
                message = "connected",
                uid = uid,
                characterId = ci.CharacterId,
                characterName = ci.CharacterName,
                characterLevel = ci.CharacterLevel,
                characterExp = ci.CharacterExp,
                characterMoney = ci.CharacterMoney,
                characterMapId = ci.CharacterMapId,
                characterLocation = new { x = ci.CharacterLocation.x, y = ci.CharacterLocation.y },
                characterHp = ci.CharacterHp,
                characterAttack = ci.CharacterAttack
            });
        }


        static async Task HandleMoveAsync(WebSocket ws, Dictionary<string, object> req)
        {
            string uid = req["uid"].ToString();
            int mapId = Convert.ToInt32(req["map_id"]);
            double dx = Convert.ToDouble(req["dx"]);
            double dy = Convert.ToDouble(req["dy"]);

            if (!maps.ContainsKey(mapId))
            {
                await SendAsync(ws, new { status = 401, message = "Invalid map id" });
                return;
            }

            var ci = clients[uid];
            // 현재 위치 꺼내오기
            var (curX, curY) = ci.CharacterLocation;
            double nx = curX + dx, ny = curY + dy;
            int mx = (int)nx, my = (int)ny;

            var grid = maps[mapId];
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

            // 위치 및 맵 ID 업데이트
            ci.CharacterLocation = (mx, my);
            ci.CharacterMapId = mapId;
            clients[uid] = ci;

            await SendAsync(ws, new
            {
                status = 201,
                characterMapId = ci.CharacterMapId,
                characterLocation = new { x = ci.CharacterLocation.x, y = ci.CharacterLocation.y }
            });
        }


        static async Task HandlePositionAsync(WebSocket ws, Dictionary<string, object> req)
        {
            string uid = req["uid"].ToString();
            if (!clients.ContainsKey(uid))
            {
                await SendAsync(ws, new { status = 400, message = "Invalid uid" });
                return;
            }

            var ci = clients[uid];
            await SendAsync(ws, new
            {
                status = 202,
                uid = uid,
                characterId = ci.CharacterId,
                characterName = ci.CharacterName,
                characterLevel = ci.CharacterLevel,
                characterExp = ci.CharacterExp,
                characterMoney = ci.CharacterMoney,
                characterMapId = ci.CharacterMapId,
                characterLocation = new { x = ci.CharacterLocation.x, y = ci.CharacterLocation.y },
                characterHp = ci.CharacterHp,
                characterAttack = ci.CharacterAttack,
                state = ci.State
            });
        }

        static async Task HandleAllAsync(WebSocket ws, Dictionary<string, object> req)
        {
            int mapId = Convert.ToInt32(req["map_id"]);
            if (!maps.ContainsKey(mapId))
            {
                await SendAsync(ws, new { status = 403, message = "Invalid map" });
                return;
            }

            var players = clients
                .Where(kv => kv.Value.CharacterMapId == mapId)
                .Select(kv => new
                {
                    uid = kv.Key,
                    characterId = kv.Value.CharacterId,
                    characterName = kv.Value.CharacterName,
                    characterLevel = kv.Value.CharacterLevel,
                    characterExp = kv.Value.CharacterExp,
                    characterMoney = kv.Value.CharacterMoney,
                    characterMapId = kv.Value.CharacterMapId,
                    characterLocation = new { x = kv.Value.CharacterLocation.x, y = kv.Value.CharacterLocation.y },
                    characterHp = kv.Value.CharacterHp,
                    characterAttack = kv.Value.CharacterAttack
                }).ToList();

            var monsters = mapMonsters[mapId]
                .Select(kv => new
                {
                    mid = kv.Key,
                    monsterId = kv.Value.MonsterId,
                    monsterName = kv.Value.MonsterName,
                    monsterLevel = kv.Value.MonsterLevel,
                    monsterCoinValue = kv.Value.MonsterCoinValue,
                    monsterMapId = kv.Value.MonsterMapId,
                    monsterLocation = new { x = kv.Value.MonsterLocation.x, y = kv.Value.MonsterLocation.y },
                    monsterHp = kv.Value.MonsterHp,
                    monsterAttackAbility = kv.Value.MonsterAttackAbility,
                    monsterDefenseAbility = kv.Value.MonsterDefenseAbility,
                    monsterExperience = kv.Value.MonsterExperience
                }).ToList();

            var items = mapItems[mapId]
                .Select(kv => new { iid = kv.Key, x = kv.Value.X, y = kv.Value.Y })
                .ToList();

            await SendAsync(ws, new
            {
                status = 203,
                body = new
                {
                    players,
                    monsters,
                    items
                }
            });
        }

        static async Task HandleRemoveAsync(WebSocket ws, Dictionary<string, object> req)
        {
            string uid = req["uid"].ToString();
            if (!clients.TryRemove(uid, out _))
            {
                await SendAsync(ws, new { status = 404, message = "Invalid uid" });
                return;
            }
            await BroadcastAsync(new { status = 204, uid = uid });

            var sessionsToRemove = combatSessions.Keys
                .Where(k => k.Split(':').Any(p => p == uid))
                .ToList();

            foreach (var key in sessionsToRemove)
            {
                var parts = key.Split(':');
                var u1 = parts[0];
                var u2 = parts[1];
                Program.RemoveCombatSession(u1, u2);

                string opponent = (u1 == uid ? u2 : u1);
                if (uidToSession.TryGetValue(opponent, out var wsOpp) && wsOpp.State == WebSocketState.Open)
                {
                    await SendAsync(wsOpp, new
                    {
                        status = 512,
                        message = "Opponent died, combat ended."
                    });
                }
            }
        }




        static async Task HandleMonsterInfoAsync(WebSocket ws, Dictionary<string, object> req)
        {
            string mid = req["mid"].ToString();
            int mapId = Convert.ToInt32(req["map_id"]);
            if (!mapMonsters.ContainsKey(mapId) || !mapMonsters[mapId].TryGetValue(mid, out var mi))
            {
                await SendAsync(ws, new { status = 405, message = "Invalid mid" });
                return;
            }
            await SendAsync(ws, new
            {
                status = 205,
                monsterId = mi.MonsterId,
                monsterName = mi.MonsterName,
                monsterLevel = mi.MonsterLevel,
                monsterCoinValue = mi.MonsterCoinValue,
                monsterMapId = mi.MonsterMapId,
                monsterLocation = new { x = mi.MonsterLocation.x, y = mi.MonsterLocation.y },
                monsterHp = mi.MonsterHp,
                monsterAttackAbility = mi.MonsterAttackAbility,
                monsterDefenseAbility = mi.MonsterDefenseAbility,
                monsterExperience = mi.MonsterExperience
            });
        }

        // --- HandlePVPRequestAsync (cmd=106): A가 전투 요청을 보낼 때 ---
        static async Task HandlePVPRequestAsync(WebSocket ws, Dictionary<string, object> req)
        {
            string u1 = req["uid1"].ToString(), u2 = req["uid2"].ToString();
            var pair = new[] { u1, u2 }; Array.Sort(pair);
            var key = $"{pair[0]}:{pair[1]}";
            combatSessions[key] = new CombatSession(pair[0], pair[1]);

            // B에게 전투 초대 알림(status=510)
            if (uidToSession.TryGetValue(u2, out var ws2) && ws2.State == WebSocketState.Open)
                await SendAsync(ws2, new { status = 510, from = u1, message = "PVP 요청이 왔습니다. 승인해 주세요." });

            // 1초 대기 후 B가 수락(cmd=115) 안 하면 타임아웃(status=511)
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                if (!combatSessions.ContainsKey(key))
                {
                    if (uidToSession.TryGetValue(u1, out var ws1) && ws1.State == WebSocketState.Open)
                        await SendAsync(ws1, new { status = 511, message = "PVP 요청 타임아웃" });
                }
            });
        }

        // --- HandlePVPAcceptAsync (cmd=115): B가 전투 요청을 수락할 때 ---
        static async Task HandlePVPAcceptAsync(WebSocket ws, Dictionary<string, object> req)
        {
            string u1 = req["uid1"].ToString(), u2 = req["uid2"].ToString();
            var pair = new[] { u1, u2 }; Array.Sort(pair);
            var key = $"{pair[0]}:{pair[1]}";

            if (combatSessions.ContainsKey(key))
            {
                // 양쪽에 전투 개시(cmd=206) 전송
                foreach (var uid in new[] { u1, u2 })
                    if (uidToSession.TryGetValue(uid, out var w) && w.State == WebSocketState.Open)
                        await SendAsync(w, new { cmd = 206, uid1 = u1, uid2 = u2 });
            }
            else
            {
                // 이미 타임아웃된 경우
                await SendAsync(ws, new { status = 511, message = "Invalid or timed-out PVP accept" });
            }
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
                await session.AddActionAsync(atk, "attack", dmg - mod);//전투시 수정
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

        /// <summary>서버→클라이언트: UID 대상에게 피해 알림 (status=502)</summary>
        public static async Task NotifyDamageAsync(string uid, int damage)
        {
            if (uidToSession.TryGetValue(uid, out var ws) && ws.State == WebSocketState.Open)
            {
                var msg = new { status = 502, damage = damage, uid = uid };
                await SendAsync(ws, msg);
            }
        }

        /// <summary>클라이언트→서버: 아이템 드랍 알림 수신 (cmd=114)</summary>
        static async Task HandleItemDropAsync(WebSocket ws, Dictionary<string, object> req)
        {
            // cmd 114: uid 또는 mid를 받아 해당 위치에 동등 스펙의 아이템 추가
            string uid = req.ContainsKey("uid") ? req["uid"].ToString() : null;
            string mid = req.ContainsKey("mid") ? req["mid"].ToString() : null;
            int mapId;
            int x, y;
            int power;
            string name;

            if (uid != null && clients.TryGetValue(uid, out var ci))
            {
                // 클라이언트 위치/맵 정보
                mapId = ci.CharacterMapId;
                x = ci.CharacterLocation.x;
                y = ci.CharacterLocation.y;

                // 드랍할 무기 스펙 (CharacterSword)
                if (ci.CharacterSword != null)
                {
                    power = ci.CharacterSword.Damage;
                    name = ci.CharacterSword.Name;
                }
                else
                {
                    power = 0;
                    name = "DroppedItem";
                }
            }
            else if (mid != null
                     && req.ContainsKey("map_id")
                     && mapMonsters.TryGetValue(Convert.ToInt32(req["map_id"]), out var mons)
                     && mons.TryGetValue(mid, out var mi))
            {
                // 몬스터 위치/맵 정보
                mapId = mi.MonsterMapId;
                x = mi.MonsterLocation.x;
                y = mi.MonsterLocation.y;

                // 드랍할 몬스터 공격력 기반
                power = mi.MonsterAttackAbility;
                name = mi.MonsterName + " Drop";
            }
            else
            {
                await SendAsync(ws, new { status = 415, message = "Invalid uid or mid" });
                return;
            }

            // 아이템 생성 및 브로드캐스트
            string iid = (nextIid++).ToString();
            var item = new ItemInfo
            {
                Iid = iid,
                MapId = mapId,
                Name = name,
                Power = power,
                X = x,
                Y = y
            };
            mapItems[mapId][iid] = item;

            await BroadcastAsync(new { status = 301, iid = iid, x = x, y = y });
        }



        /// <summary>서버→클라이언트: 객체 사망 알림 (status=300)</summary>
        public static async Task NotifyDeathAsync(string uid)
        {
            if (uidToSession.TryGetValue(uid, out var ws) && ws.State == WebSocketState.Open)
            {
                var msg = new { status = 300, uid = uid };
                await SendAsync(ws, msg);
            }
        }

    }

}
*/