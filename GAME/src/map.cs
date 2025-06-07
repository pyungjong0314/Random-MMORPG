using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
    TileFactory에서 다양한 종류의 타일(예: 물, 풀)을 만들고,  
    이를 TileGrid에 배치하여 타일 격자 형태로 구성한 후,  
    최종적으로 해당 타일 격자를 Map에 넣어 하나의 지도를 구성할 예정.  
    MapFactory를 통해 다양한 종류의 맵을 구현할 예정.
*/


namespace Game.Maps
{
    public class Map
    {
        string map_name;
        int map_id;
        int map_width;
        int map_height;


     /* 
        TileGrid map_tile;
        Character[] map_character_list;
        Monster[] map_monster_list;
        Weapon[] map_weapon_list;
        Money[] map_money_list;
        Exp[] map_exp_list;
     */


        public static MapFactory()
        {
            public static Map DungeonMapCreate()
            {
                
            }

            public static Map FieldMapCreate()
            {

            }

            public static Map TownMapCreate()
            {

            }


            public static Map CreateMap(int map_id)
            {
                switch (map_id)
                {
                    case 0:
                        return DungeonMapCreate();
                    case 1:
                        return FieldMapCreate();
                    case 2:
                        return TownMapCreate();
                    default:
                        throw new ArgumentException("Invalid monster type");
                }
            }
        }
    }

    public class TileGrid
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
    }
}