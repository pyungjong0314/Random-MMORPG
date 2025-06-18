using System;
using System.Collections.Generic;
using Game.Monsters;
using Game.BossMonsters;
using Game.BaseMonster;
using System.Drawing;
using System.Windows.Forms;
using Game.MonsterManagers;
using Game.Characters;

namespace Game.Maps
{
    public class Map
    {
        public void Initialize(List<Monster> monsters)
        {
            Monsters = monsters;
            foreach (var monster in Monsters)
            {
                monster.MapRef = this;
            }
        }

        public string map_name;
        public int map_id;
        public int map_width;
        public int map_height;


        // 맵에 존재하는 몬스터 리스트
        public List<Monster> Monsters { get; private set; } = new List<Monster>();

        // 맵에 드랍된 코인 리스트
        public List<(int x, int y, int amount)> DroppedCoins { get; private set; } = new List<(int x, int y, int amount)>();

        // 맵에 존재하는 캐릭터 리스트
        public List<Character> opponentCharacters { get; private set; } = new List<Character>();

        public Map() { }

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
    }
}
