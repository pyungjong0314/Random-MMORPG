using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Monsters;



/*
 *  TileFactory���� �پ��� ������ Ÿ��(��: ��, Ǯ)�� �����,
�̸� TileGrid�� ��ġ�Ͽ� Ÿ�� ���� ���·� ������ ��,
���������� �ش� Ÿ�� ���ڸ� Map�� �־� �ϳ��� ������ ������ ����.  
    MapFactory�� ���� �پ��� ������ ���� ������ ����.
*/


namespace Game.Maps
{
    public class Map
    {
        public string map_name;
        public int map_id;
        public int map_width;
        public int map_height;



        // ���� ���� ���� ��ǥ
        // ���� ���� ���� ��ǥ
        public Dictionary<string, List<(int x, int y)>> MonsterLocations = new Dictionary<string, List<(int x, int y)>>()
        {
            ["first_base_monster"] = new List<(int, int)>
            {
                (10, 20), (20, 20), (30, 20), (40, 20), (50, 20)
            },
            ["second_base_monster"] = new List<(int, int)>
            {
                (20, 50), (30, 50), (40, 50), (50, 50), (60, 50)
            },
            ["boss_monster"] = new List<(int, int)>
            {
                (50, 100)
            }
        };



        // ������ ���� ����Ʈ
        private List<(Type monsterType, (int x, int y) location, int countdown)> respawnQueue
            = new List<(Type, (int, int), int)>();



        // ���� �ٽ� ����
        public void RequestRespawn(Type monsterType, (int x, int y) location, int delayInSeconds)
        {
            respawnQueue.Add((monsterType, location, delayInSeconds));
        }


        // ���� ����
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

            // �ʿ� �� ���� ���͵� �߰�
            throw new Exception("Unknown monster type");
        }





        // �� ������Ʈ �Լ����� ī��Ʈ ���̱� + ������ ����
        public void Update()
        {
            for (int i = respawnQueue.Count - 1; i >= 0; i--)
            {
                var item = respawnQueue[i];
                if (item.countdown <= 1)
                {
                    Monster newMonster = CreateMonsterFromType(item.monsterType);
                    AddMonster(newMonster, item.location.x, item.location.y);
                    respawnQueue.RemoveAt(i);
                }
                else
                {
                    respawnQueue[i] = (item.monsterType, item.location, item.countdown - 1);
                }
            }
        }





        public Map() { }
        /*   TileGrid map_tile;
           Character[] map_character_list;
           Weapon[] map_weapon_list;
           Money[] map_money_list;
           Exp[] map_exp_list;*/


        // ���͸� ������ ����Ʈ
        public List<Monster> monsters = new List<Monster>();


        public Map MapRef { get; set; } // ���Ͱ� �ڽ��� �Ҽӵ� ���� �����

        public void AddMonster(Monster monster, int x, int y)
        {
            monster.SetLocation(x, y);
            monster.MapRef = this; // �Ҽ� �� ����
            monsters.Add(monster);
        }


        public void RemoveMonster(Monster m)
        {
            monsters.Remove(m);   
            Console.WriteLine($"{m.MonsterName} {m.MonsterId} has died!");
            Console.WriteLine($"{m.MonsterName} dropped {m.MonsterCoinValue} coins");
            // ���⵵ ����ϵ��� ���� ����
        }
    }
    public static class MapFactory
    {

        private static void BaseMap(Map map)
        {
            // ���������� ��� �� ũ�⸦ �����ϰ� ����
            map.map_width = 100;
            map.map_height = 100;
        }


        // ��� �ʸ��� �⺻ ���� 2���� 5������, ������ 1���� ����
        private static Map DungeonMapCreate()
        {
            Map m = new Map();
            m.map_id = 1;



            foreach (var spawn in m.MonsterLocations["first_base_monster"])
            {
                m.AddMonster(new Goblin(), spawn.x,  spawn.y);
            }


            foreach (var spawn in m.MonsterLocations["second_base_monster"])
            {
                m.AddMonster(new Slime(), spawn.x, spawn.y);
            }

            foreach (var spawn in m.MonsterLocations["boss_monster"])
            {
                m.AddMonster(new GoblinKing(), spawn.x, spawn.y);
            }

            return m;
        }

        private static Map FieldMapCreate()
        {
            Map m = new Map();
            m.map_id = 2;


            foreach (var spawn in m.MonsterLocations["first_base_monster"])
            {
                m.AddMonster(new Witch(), spawn.x, spawn.y);
            }


            foreach (var spawn in m.MonsterLocations["second_base_monster"])
            {
                m.AddMonster(new Basilisk(), spawn.x, spawn.y);
            }


            foreach (var spawn in m.MonsterLocations["boss_monster"])
            {
                m.AddMonster(new LunaCrab(), spawn.x, spawn.y);
            }

            
            return m;
        }

        private static Map TownMapCreate()
        {
            Map m = new Map();
            m.map_id = 3;

            foreach (var spawn in m.MonsterLocations["first_base_monster"])
            {
                m.AddMonster(new Scorpion(), spawn.x, spawn.y);
            }


            foreach (var spawn in m.MonsterLocations["second_base_monster"])
            {
                m.AddMonster(new Orc(), spawn.x, spawn.y);
            }


            foreach (var spawn in m.MonsterLocations["boss_monster"])
            {
                m.AddMonster(new DarkKnight(), spawn.x, spawn.y);
            }


            return m;
        }


        public static Map CreateMap(int map_id)
        {
            switch (map_id)
            {
                case 1:
                    return DungeonMapCreate();
                case 2:
                    return FieldMapCreate();
                case 3:
                    return TownMapCreate();
                default:
                    throw new ArgumentException("Invalid monster type");
            }
        }
    }




    /*  public class TileGrid
      {
          public Tile[,] Grid;

          public TileGrid(int width, int height)
          {
              Grid = new Tile[width, height];
          }

          public void SetTile(int x, int y, Tile tile)
          {
              Grid[x, y] = tile;
          }

          public Tile GetTile(int x, int y) => Grid[x, y];
      }


      public class Tile
      {
          public int tile_x, tile_y;
          public string tile_type;
          public bool tile_is_walkable;



          public static class TileFactory
          {
              public static Tile CreateGrassTile() => new Tile("Grass", true);
              public static Tile CreateWaterTile() => new Tile("Water", false);
          }
      }*/
}
