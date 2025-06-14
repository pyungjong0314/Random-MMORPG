using System;
using System.Collections.Generic;
using Game.Monsters;
using Game.BossMonsters;
using Game.BaseMonster;
using System.Drawing;
using Game.Characters;


namespace Game.Maps
{
    public class Map
    {

        public string map_name;
        public int map_id;
        public int map_width;
        public int map_height;

        
        // 현재 맵에 존재하는 몬스터 리스트
        public List<Monster> Monsters { get; private set; } = new List<Monster>();



        // 맵에 드랍된 코인 리스트
        public List<(int x, int y, int amount)> DroppedCoins { get; private set; } = new List<(int x, int y, int amount)>();



        // 리스폰 대기 큐 (몬스터 타입, 리스폰까지 남은 시간)
        private List<(Type monsterType, (int x, int y) location, int countdown)> respawnQueue =
            new List<(Type monsterType, (int x, int y) location, int countdown)>();


        // 몬스터 리스폰 요청 (타입, 위치, 지연 시간 지정)
        public void RequestRespawn(Type monsterType, (int x, int y) location, int delayInSeconds)
        {
            respawnQueue.Add((monsterType, location, delayInSeconds));
            Console.WriteLine($"{monsterType} has respwan ({location.x},{location.y}) after {delayInSeconds}s");
        }

        // 매 프레임마다 리스폰 큐 업데이트 및 처리
        public void Update()
        {
            for (int i = respawnQueue.Count - 1; i >= 0; i--)
            {
                var item = respawnQueue[i];
                if (item.countdown <= 1)
                {
                    Monster newMonster = CreateMonsterFromType(item.monsterType);
                    newMonster.MonsterLocation = item.location;
                    AddMonster(newMonster);
                    respawnQueue.RemoveAt(i);
                }
                else
                {
                    respawnQueue[i] = (item.monsterType, item.location, item.countdown - 1);
                }
            }
        }


        public Map() { }



        // 몬스터를 맵에 추가하고 랜덤 좌표 지정
        public void AddMonster(Monster m)
        {
            if (m.MonsterLocation == default)
                m.MonsterLocation = GetRandomLocation();

            m.MapRef = this;
            Monsters.Add(m);
        }



        // 몬스터 맵에서 제거 및 코인 드랍
        public void RemoveMonster(Monster m)
        {
            Monsters.Remove(m);
            DroppedCoins.Add((m.MonsterLocation.x, m.MonsterLocation.y, m.MonsterCoinValue));
            

            Console.WriteLine($"{m.MonsterName} has dropped  {m.MonsterCoinValue} coins  and {m.MonsterExperience} exp");
        }

        
        // 떨어진 코인 줍는 로직
        public (int totalAmount, int count) PickUpCoins((int x, int y) location)
        {
            int pickupRange = 40;
            int total = 0;
            int count = 0;

            DroppedCoins.RemoveAll(c =>
            {
                double dist = Math.Sqrt(Math.Pow(c.x - location.x, 2) + Math.Pow(c.y - location.y, 2));
                if (dist <= pickupRange)
                {
                    total += c.amount;
                    count++;
                    return true;
                }
                return false;
            });

            return (total, count);
        }





        // 타입에 따른 몬스터 생성기
        private Monster CreateMonsterFromType(Type monsterType)
        {
            if (monsterType == typeof(Goblin)) return new Goblin();
            if (monsterType == typeof(Slime)) return new Slime();
            if (monsterType == typeof(Scorpion)) return new Scorpion();
            if (monsterType == typeof(Witch)) return new Witch();
            if (monsterType == typeof(Basilisk)) return new Basilisk();
            if (monsterType == typeof(Orc)) return new Orc();
            if (monsterType == typeof(LunaCrab)) return new LunaCrab();
            if (monsterType == typeof(GoblinKing)) return new GoblinKing();
            if (monsterType == typeof(DarkKnight)) return new DarkKnight();

            throw new Exception("Unknown monster type");
        }

        // 랜덤 좌표 생성기
        private (int x, int y) GetRandomLocation()
        {
            Random rnd = new Random();
            return (rnd.Next(0, 1000), rnd.Next(0, 1000));
        }
    }

    public static class MapFactory
    {
        // 맵 ID에 따라 다양한 몬스터들이 배치된 맵 생성
        public static Map CreateMap(int map_id)
        {
            Map map = new Map { map_id = map_id, map_width = 1000, map_height = 1000 };

            switch (map_id)
            {
                case 1:
                    var goblin1 = new Goblin();
                    goblin1.MonsterLocation = (100, 200);

                    var goblin2 = new Goblin();
                    goblin2.MonsterLocation = (100, 300);

                    var goblin3 = new Goblin();
                    goblin3.MonsterLocation = (100, 400);



                    var scorpion1 = new Scorpion();
                    scorpion1.MonsterLocation = (300, 200);

                    var scorpion2 = new Scorpion();
                    scorpion2.MonsterLocation = (300, 300);

                    var scorpion3 = new Scorpion();
                    scorpion3.MonsterLocation = (300, 400);



                    var witch1 = new Witch();
                    witch1.MonsterLocation = (500, 200);

                    var witch2= new Witch();
                    witch2.MonsterLocation = (500, 300);

                    var witch3= new Witch();
                    witch3.MonsterLocation = (500, 400);


                    AddMonsters(map, new List<Monster> { goblin1, goblin2, goblin3, scorpion1, scorpion2, scorpion3, witch1, witch2, witch3 });
                    break;

                case 2:
                    AddMonsters(map, new List<Monster>
                    {
                        new Witch(), new Witch(), new Basilisk(), new Basilisk(), new LunaCrab()
                    });
                    break;

                case 3:
                    AddMonsters(map, new List<Monster>
                    {
                        new Scorpion(), new Scorpion(), new Orc(), new Orc(), new DarkKnight()
                    });
                    break;

                default:
                    throw new ArgumentException("Invalid map ID");
            }

            return map;
        }

        // 몬스터 리스트를 받아 맵에 추가
        private static void AddMonsters(Map map, List<Monster> monsters)
        {
            foreach (var monster in monsters)
            {
                map.AddMonster(monster);
            }
        }
    }
}
