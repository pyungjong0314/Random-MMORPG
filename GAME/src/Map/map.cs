using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Monsters;



/*
 *  TileFactory에서 다양한 종류의 타일(예: 물, 풀)을 만들고,
이를 TileGrid에 배치하여 타일 격자 형태로 구성한 후,
최종적으로 해당 타일 격자를 Map에 넣어 하나의 지도를 구성할 예정.  
    MapFactory를 통해 다양한 종류의 맵을 구현할 예정.
*/


namespace Game.Maps
{
    public class Map
    {
        public string map_name;
        public int map_id;
        public int map_width;
        public int map_height;


        public Map() { }
        /*   TileGrid map_tile;
           Character[] map_character_list;
           Weapon[] map_weapon_list;
           Money[] map_money_list;
           Exp[] map_exp_list;*/


        // 몬스터를 저장할 리스트
        public List<Monster> monsters = new List<Monster>();


        public Map MapRef { get; set; } // 몬스터가 자신이 소속된 맵을 기억함

        public void AddMonster(Monster monster, int x, int y)
        {
            monster.SetLocation(x, y);
            monster.MapRef = this; // 소속 맵 설정
            monsters.Add(monster);
        }


        public void RemoveMonster(Monster m)
        {
            monsters.Remove(m);
            Console.WriteLine($"{m.MonsterName} removed from map.");
        }
    }
    public static class MapFactory
    {

        private static void BaseMap(Map map)
        {
            // 임의적으로 모든 맵 크기를 동일하게 설정
            map.map_width = 100;
            map.map_height = 100;
        }


        // 모든 맵마다 기본 몬스터 2종류 5마리씩, 보스몹 1마리 생성
        private static Map DungeonMapCreate()
        {
            Map m = new Map();
            m.map_id = 1;

            for (int i = 0; i < 5; i++)
            {
                m.AddMonster(new Goblin(), i * 3, i * 3);
                m.AddMonster(new Slime(), i * 7, i * 7);
            }

            m.AddMonster(new GoblinKing(), 50, 100);

            return m;
        }

        private static Map FieldMapCreate()
        {
            Map m = new Map();
            m.map_id = 2;

            for (int i = 0; i < 5; i++)
            {
                m.AddMonster(new Witch(), i * 3, i * 3);
                m.AddMonster(new Basilisk(), i * 7, i * 7);
            }

            m.AddMonster(new LunaCrab(), 50, 100);
            
            return m;
        }

        private static Map TownMapCreate()
        {
            Map m = new Map();
            m.map_id = 3;

            for (int i = 0; i < 5; i++)
            {
                m.AddMonster(new Scorpion(), i * 3, i * 3);
                m.AddMonster(new Orc(), i * 7, i * 7);
            }

            m.AddMonster(new DarkKnight(), 50, 100);
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
