// -----------------------------
// TCP Server Side
// -----------------------------
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;


namespace MonsterServer
{

    public class TcpMonsterServer
    {
        private TcpListener listener;
        private bool isRunning = false;

        // 몬스터 데이터 저장소
        private Dictionary<int, List<Monster>> mapMonsters;
        private Dictionary<int, HashSet<(int, int)>> usedPositions;
        private Dictionary<string, Func<Monster>> monsterFactories;
        private Random rng = new Random();

        public TcpMonsterServer()
        {
            mapMonsters = new Dictionary<int, List<Monster>>();
            usedPositions = new Dictionary<int, HashSet<(int, int)>>();

            // 몬스터 타입별 생성자 매핑
            monsterFactories = new Dictionary<string, Func<Monster>>
        {
            { "goblin",      () => new Goblin() },
            { "slime",       () => new Slime() },
            { "goblinking",  () => new GoblinKing() },
            { "orc",         () => new Orc() },
            { "scorpion",    () => new Scorpion() },
            { "lunacrab",    () => new LunaCrab() },
            { "witch",       () => new Witch() },
            { "basilisk",    () => new Basilisk() },
            { "darkknight",  () => new DarkKnight() }
        };

            InitializeMonstersPerMap(); // 초기 몬스터 배치
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, 7777);
            listener.Start();
            isRunning = true;
            Console.WriteLine("Server started on port 7777...");

            while (isRunning)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            string jsonRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            var request = JsonSerializer.Deserialize<MonsterRequest>(jsonRequest);

            // 요청된 몬스터 타입 필터링
            List<Monster> result = mapMonsters.ContainsKey(request.mapId)
                ? mapMonsters[request.mapId].FindAll(m => m.MonsterId.StartsWith(request.type))
                : new List<Monster>();

            var response = new MonsterResponse
            {
                status = 200,
                count = result.Count
            };

            string jsonResponse = JsonSerializer.Serialize(response);
            byte[] sendData = Encoding.UTF8.GetBytes(jsonResponse);
            stream.Write(sendData, 0, sendData.Length);

            client.Close();
        }

        private void InitializeMonstersPerMap()
        {
            mapMonsters[1] = CreateMonsterBatch(1, new[] { "goblin", "slime", "goblinking" }, 100);
            mapMonsters[2] = CreateMonsterBatch(2, new[] { "orc", "scorpion", "lunacrab" }, 100);
            mapMonsters[3] = CreateMonsterBatch(3, new[] { "witch", "basilisk", "darkknight" }, 100);
        }

        private List<Monster> CreateMonsterBatch(int mapId, string[] monsterKeys, int countPerType)
        {
            var monsters = new List<Monster>();
            var positions = new HashSet<(int, int)>();
            usedPositions[mapId] = positions;

            foreach (var key in monsterKeys)
            {
                for (int i = 0; i < countPerType; i++)
                {
                    var monster = monsterFactories[key]();
                    monster.MonsterId += $"_{Guid.NewGuid().ToString().Substring(0, 8)}";
                    monster.MonsterMapId = mapId;

                    (int x, int y) pos;
                    do
                    {
                        pos = (rng.Next(0, 100), rng.Next(0, 100));
                    }
                    while (positions.Contains(pos));

                    monster.MonsterLocation = pos;
                    positions.Add(pos);
                    monsters.Add(monster);
                }
            }

            return monsters;
        }
    }


}
