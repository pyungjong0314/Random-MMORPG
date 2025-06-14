using System;
using System.Collections.Generic;
using Game.Monsters;
using Game.BossMonsters;
using Game.BaseMonster;
using System.Drawing;
using Game.Characters;
using System.Windows.Forms;


namespace Game.Maps
{
    public class Map
    {

        public string map_name;
        public int map_id;
        public int map_width;
        public int map_height;


        // 맵에 존재하는 몬스터 리스트
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
            Console.WriteLine($"{monsterType} is respawning at ({location.x},{location.y}) after {delayInSeconds}s");
        }



        // 매 프레임마다 리스폰 큐 업데이트 및 처리
        public PictureBox Update()
        {

            // 슬라임 3번 죽여야 제거됨
            PictureBox pb = new PictureBox();

            for (int i = respawnQueue.Count - 1; i >= 0; i--)
            {
                var item = respawnQueue[i];
                if (item.countdown <= 1)
                {
                    Monster newMonster = CreateMonsterFromType(item.monsterType);
                    newMonster.MonsterLocation = item.location;
                    AddMonster(newMonster);
                    respawnQueue.RemoveAt(i);

                    pb.Image = CreateImageFromType(item.monsterType);
                    pb.Size = new Size(40, 40); // 이미지 크기 설정
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BackColor = Color.Transparent;

                    pb.Location = new Point(newMonster.MonsterLocation.x, newMonster.MonsterLocation.y);
                    pb.Tag = newMonster;

                    return pb;
                    }
                else
                {
                    respawnQueue[i] = (item.monsterType, item.location, item.countdown - 1);
                }
            }
            return null;
        }


        public Map() { }



        // 몬스터를 맵에 추가하고 랜덤 좌표 지정
        public void AddMonster(Monster m)
        { 
            m.MapRef = this;
            Monsters.Add(m);
        }



        // 몬스터 맵에서 제거 및 코인 드랍
        public void RemoveMonster(Monster m, Form form)
        {
            Monsters.Remove(m);
            DroppedCoins.Add((m.MonsterLocation.x, m.MonsterLocation.y, m.MonsterCoinValue));  // 코인 맵에 드랍하기
            Console.WriteLine($"{m.MonsterName} has dropped  {m.MonsterCoinValue} coins  and {m.MonsterExperience} exp");

            // PictureBox 제거
            foreach (Control control in form.Controls)
            {
                if (control is PictureBox pb && pb.Tag == m)
                {
                    form.Controls.Remove(pb);
                    pb.Dispose();
                    break;
                }
            }
        }

        public Image CreateImageFromType(Type monsterType)
        {
            if (monsterType == typeof(Goblin)) return Image.FromFile("goblin1.png");
            if (monsterType == typeof(Slime)) return Image.FromFile("C:\\Users\\me\\Desktop\\MMORPG\\Random-MMORPG\\GAME\\src\\Resources\\slime.png"); ;
            if (monsterType == typeof(Scorpion)) return Image.FromFile("scorpion.png");
            if (monsterType == typeof(Witch)) return Image.FromFile("wizard.png");
            return Image.FromFile("goblin1.png");

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
        public Monster CreateMonsterFromType(Type monsterType)
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

            var mapDefinitions = new Dictionary<int, List<(Type type, int count)>>
            {
                { 1, new List<(Type, int)> { (typeof(Goblin), 6), (typeof(Slime), 10) } },
                { 2, new List<(Type, int)> { (typeof(Witch), 2), (typeof(Basilisk), 2), (typeof(LunaCrab), 1) } },
                { 3, new List<(Type, int)> { (typeof(Scorpion), 2), (typeof(Orc), 2), (typeof(DarkKnight), 1) } }
            };

            if (!mapDefinitions.TryGetValue(map_id, out var monsterList))
                throw new ArgumentException("Invalid map ID");

            foreach (var (type, count) in monsterList)
            {
                for (int i = 0; i < count; i++)
                {
                    var monster = map.CreateMonsterFromType(type);
                    map.AddMonster(monster); // 랜덤 위치 부여됨
                }
            }

            return map;
        }
    }
}
