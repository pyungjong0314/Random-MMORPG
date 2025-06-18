// Program.cs
// Requires Newtonsoft.Json (Json.NET) package. Install via NuGet: Install-Package Newtonsoft.Json

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace client
{
    #region DTO 정의 (CMD 103)
    class AllResponse103 { public int status; public Body103 body; }
    class Body103
    {
        public List<PlayerEntry> players { get; set; }
        public List<MonsterEntry> monsters { get; set; }
        public List<ItemEntry> items { get; set; }
    }
    class PlayerEntry
    {
        public string uid;
        public int characterId;
        public string characterName;
        public int characterLevel;
        public int characterExp;
        public int characterMoney;
        public int characterMapId;
        public Location characterLocation;
        public int characterHp;
        public int characterAttack;
    }
    class MonsterEntry
    {
        public string mid;
        public string monsterId;
        public string monsterName;
        public int monsterLevel;
        public int monsterCoinValue;
        public int monsterMapId;
        public Location monsterLocation;
        public int monsterHp;
        public int monsterAttackAbility;
        public int monsterDefenseAbility;
        public int monsterExperience;
    }
    class ItemEntry
    {
        public string iid;
        public double x;   // double 로 변경!
        public double y;   // double 로 변경!
    }
    class Location { public int x; public int y; }
    #endregion

    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(new Uri("ws://localhost:25565/ws/"), CancellationToken.None);

                // 100: Connect
                Console.WriteLine("===================== CMD 100 =====================");
                Console.WriteLine("명령 100: 서버에 연결하고 새로운 UID를 요청합니다");
                var json100 = await SendAndReceive(ws, new { cmd = 100, uid = "" });
                dynamic o100 = JsonConvert.DeserializeObject(json100);
                string myUid = o100.uid;
                Console.WriteLine($"→ 할당된 UID = {myUid}");

                // 101: Move
                Console.WriteLine("===================== CMD 101 =====================");
                Console.WriteLine("명령 101: 캐릭터를 (dx,dy) 만큼 이동시킵니다");
                await SendAndReceive(ws, new { cmd = 101, uid = myUid, map_id = 1, dx = 1, dy = 0 });

                // 102: Position
                Console.WriteLine("===================== CMD 102 =====================");
                Console.WriteLine("명령 102: 현재 자신의 위치와 상태를 조회합니다");
                await SendAndReceive(ws, new { cmd = 102, uid = myUid });

                // 103: All
                Console.WriteLine("===================== CMD 103 =====================");
                Console.WriteLine("명령 103: 맵 전체(플레이어/몬스터/아이템) 정보를 가져옵니다");
                var json103 = await SendAndReceive(ws, new { cmd = 103, map_id = 1 });

                // 파싱 & 출력
                var resp103 = JsonConvert.DeserializeObject<AllResponse103>(json103);
                Console.WriteLine("----- Players -----");
                foreach (var p in resp103.body.players)
                {
                    Console.WriteLine(
                        $"uid={p.uid}, id={p.characterId}, name={p.characterName}, lvl={p.characterLevel}, " +
                        $"exp={p.characterExp}, money={p.characterMoney}, map={p.characterMapId}, " +
                        $"loc=({p.characterLocation.x},{p.characterLocation.y}), hp={p.characterHp}, atk={p.characterAttack}"
                    );
                }
                Console.WriteLine("----- Monsters -----");
                foreach (var m in resp103.body.monsters)
                {
                    Console.WriteLine(
                        $"mid={m.mid}, name={m.monsterName}, lvl={m.monsterLevel}, coins={m.monsterCoinValue}, " +
                        $"map={m.monsterMapId}, loc=({m.monsterLocation.x},{m.monsterLocation.y}), " +
                        $"hp={m.monsterHp}, atk={m.monsterAttackAbility}, def={m.monsterDefenseAbility}, exp={m.monsterExperience}"
                    );
                }

                // 105: MonsterInfo
                Console.WriteLine("===================== CMD 105 =====================");
                Console.WriteLine("명령 105: 특정 몬스터의 상세 정보를 조회합니다");
                await SendAndReceive(ws, new { cmd = 105, mid = resp103.body.monsters[0].mid, map_id = 1 });

                // 104: Remove
                Console.WriteLine("===================== CMD 104 =====================");
                Console.WriteLine("명령 104: 자신의 객체(로그아웃)를 제거합니다");
                await SendAndReceive(ws, new { cmd = 104, uid = myUid });

                Console.WriteLine("====== 테스트 완료 ======");
            }
        }


        static async Task<string> SendAndReceive(ClientWebSocket ws, object message)
        {
            // 보낸 메시지
            var json = JsonConvert.SerializeObject(message);
            Console.WriteLine($">>> 보내는 메시지: {json}");

            // 전송
            var bytes = Encoding.UTF8.GetBytes(json);
            await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

            // 수신
            var buf = new ArraySegment<byte>(new byte[8192]);
            var res = await ws.ReceiveAsync(buf, CancellationToken.None);
            var respJson = Encoding.UTF8.GetString(buf.Array, 0, res.Count);

            // 받은 메시지
            Console.WriteLine($"<<< 받은 메시지: {respJson}\n");
            return respJson;
        }
    }
}
