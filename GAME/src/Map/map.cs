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
            Console.WriteLine($"{m.MonsterName} removed from map.");
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
