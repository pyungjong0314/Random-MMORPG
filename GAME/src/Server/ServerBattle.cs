/*// Program.cs
// Requires Newtonsoft.Json (Json.NET) package. Install via NuGet: Install-Package Newtonsoft.Json
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WindowsFormsApp1.Client
{
    #region DTO 정의 (CMD 103 + PvP)
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
    class ItemEntry { public string iid; public double x; public double y; }
    class Location { public int x; public int y; }

    // PvP 관련 응답용 DTO
    public class CombatResponse
    {
        public int status { get; set; }
        public int cmd { get; set; }
        public int my_damage { get; set; }
        public int target_damage { get; set; }
        public string from { get; set; }
        public string message { get; set; }
        public string uid1 { get; set; }
        public string uid2 { get; set; }
    }
    #endregion

    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var wsA = new ClientWebSocket())
            using (var wsB = new ClientWebSocket())
            {
                // 1) 서버 연결
                Console.WriteLine("=== 서버에 연결 중 ===");
                await wsA.ConnectAsync(new Uri("ws://localhost:25565/ws/"), CancellationToken.None);
                await wsB.ConnectAsync(new Uri("ws://localhost:25565/ws/"), CancellationToken.None);
                Console.WriteLine();

                // 2) cmd=100: UID 할당
                string uidA = await ConnectAndGetUid(wsA, "[A]");
                string uidB = await ConnectAndGetUid(wsB, "[B]");

                // 3) (원하시면 cmd=101~103 등 추가 요청 삽입)

                //
                // --- PvP 전투 테스트 (동시 요청으로 교착 해소) ---
                //
                // 4) A → B 전투 요청 (cmd=106)
                Console.WriteLine("===================== PvP Test =====================");
                Console.WriteLine("명령 106: A → B 전투 요청");
                await SendRaw(wsA, new { cmd = 106, uid1 = uidA, uid2 = uidB }, "[A]");

                // 5) B: 초대 수신(status==510) 대기
                while (true)
                {
                    var msg = await ReceiveOnce(wsB, "[B]");
                    var inv = JsonConvert.DeserializeObject<CombatResponse>(msg);
                    if (inv.status == 510)
                    {
                        Console.WriteLine("[B] Invitation received");
                        break;
                    }
                }

                // 6) B: 수락(cmd=115)
                Console.WriteLine("명령 115: B 전투 수락");
                await SendRaw(wsB, new { cmd = 115, uid1 = uidA, uid2 = uidB }, "[B]");

                // 7) 양쪽: PVP 시작(cmd=206) 대기
                await WaitForCmd(wsA, 206, "[A]");
                await WaitForCmd(wsB, 206, "[B]");
                Console.WriteLine("→ PVP 시작됨\n");

                // 8) 전투 루프 (동시 공격/방어 → 응답 동시 수신)
                int hpA = 20, hpB = 20;
                while (hpA > 0 && hpB > 0)
                {
                    // A 공격, B 방어 요청 연달아
                    var atk = new { cmd = 108, damage = 5, modifier = 0, target_uid = uidB };
                    var def = new { cmd = 109, modifier = 2, target_uid = uidA };
                    await SendRaw(wsA, atk, "[A]");
                    await SendRaw(wsB, def, "[B]");

                    // 두 응답을 병렬 수신
                    var taskA = ReceiveOnce(wsA, "[A]");
                    var taskB = ReceiveOnce(wsB, "[B]");
                    await Task.WhenAll(taskA, taskB);

                    var resA = JsonConvert.DeserializeObject<CombatResponse>(taskA.Result);
                    var resB = JsonConvert.DeserializeObject<CombatResponse>(taskB.Result);

                    // HP 업데이트
                    hpA -= resA.my_damage + resB.target_damage;
                    hpB -= resA.target_damage + resB.my_damage;
                    Console.WriteLine($"HP → A:{hpA}, B:{hpB}\n");
                }

                // 9) 사망자 제거 요청(cmd=104)
                if (hpB <= 0)
                {
                    Console.WriteLine("[A] B 사망, 제거 요청");
                    await SendRaw(wsA, new { cmd = 104, uid = uidB }, "[A]");
                }
                else
                {
                    Console.WriteLine("[B] A 사망, 제거 요청");
                    await SendRaw(wsB, new { cmd = 104, uid = uidA }, "[B]");
                }

                // 10) 제거 완료(status=204) 또는 combat 종료(status=512) 대기
                await WaitForStatuses(wsA, new[] { 204, 512 }, "[A]");
                await WaitForStatuses(wsB, new[] { 204, 512 }, "[B]");

                Console.WriteLine("====== PVP 테스트 완료 ======");
            }
        }

        // UID 획득 헬퍼
        static async Task<string> ConnectAndGetUid(ClientWebSocket ws, string tag)
        {
            var resp = await SendAndReceive(ws, new { cmd = 100, uid = "" }, tag);
            dynamic o = JsonConvert.DeserializeObject(resp);
            string uid = o.uid;
            Console.WriteLine($"{tag} 할당된 UID = {uid}\n");
            return uid;
        }

        // JSON 직렬화 후 SendAsync (태그 로그 포함)
        static async Task SendRaw(ClientWebSocket ws, object msgObj, string tag)
        {
            string json = JsonConvert.SerializeObject(msgObj);
            Console.WriteLine($"{tag} >>> {json}");
            var buf = Encoding.UTF8.GetBytes(json);
            await ws.SendAsync(new ArraySegment<byte>(buf), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // SendRaw + ReceiveOnce
        static async Task<string> SendAndReceive(ClientWebSocket ws, object msgObj, string tag)
        {
            await SendRaw(ws, msgObj, tag);
            return await ReceiveOnce(ws, tag);
        }

        // 한 번 ReceiveAsync 실행 후 텍스트 리턴
        static async Task<string> ReceiveOnce(ClientWebSocket ws, string tag)
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            var res = await ws.ReceiveAsync(buffer, CancellationToken.None);
            string txt = Encoding.UTF8.GetString(buffer.Array, 0, res.Count);
            Console.WriteLine($"{tag} <<< {txt}\n");
            return txt;
        }

        // 특정 cmd 값이 올 때까지 대기
        static async Task WaitForCmd(ClientWebSocket ws, int wantedCmd, string tag)
        {
            while (true)
            {
                var msg = await ReceiveOnce(ws, tag);
                var cr = JsonConvert.DeserializeObject<CombatResponse>(msg);
                if (cr.cmd == wantedCmd) return;
            }
        }

        // 특정 status 값들 중 하나가 올 때까지 대기
        static async Task WaitForStatuses(ClientWebSocket ws, int[] statuses, string tag)
        {
            while (true)
            {
                var msg = await ReceiveOnce(ws, tag);
                var cr = JsonConvert.DeserializeObject<CombatResponse>(msg);
                foreach (var st in statuses)
                    if (cr.status == st)
                        return;
            }
        }
    }
}
*/