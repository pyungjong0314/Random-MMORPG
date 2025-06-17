using Game.BaseMonster;
using Game.Maps;
using Game.MonsterManagers;
using Game.Obstacles;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game.MapFactories
{
    public static class MapFactory
    {
        public static (Map map, Image mapImage) CreateMap(List<Monster> monsters, List<Obstacle> obstacles)
        {
            // 1. Map 객체 생성 및 몬스터 초기화
            Map map = new Map();
            map.Initialize(monsters);

            // 2. 몬스터까지 그려진 이미지 생성
            Image mapImage = CreateMapImage(monsters, obstacles);

            return (map, mapImage);
        }

        private static Image CreateMapImage(List<Monster> monsters, List<Obstacle> obstacles)
        {
            Bitmap bmp = new Bitmap(1000, 1000);

            using (Graphics g = Graphics.FromImage(bmp))
            {
      /*          // 1. 배경 그리기
                g.Clear(Color.LightGreen);
                for (int x = 0; x < 1000; x += 100)
                {
                    for (int y = 0; y < 1000; y += 100)
                    {
                        g.DrawRectangle(Pens.Gray, x, y, 100, 100);
                    }
                }*/

                // 1. 몬스터 그리기
                foreach (var monster in monsters)
                {
                    Image monsterImg = MonsterManager.CreateImageFromType(monster.GetType());
                    g.DrawImage(monsterImg, monster.MonsterLocation.x, monster.MonsterLocation.y, 64, 64);
                }


                // 2. 장애물 그리기
                foreach (var obstacle in obstacles)
                {
                    Image obstacleImg = ObstacleManager.CreateImageFromType(obstacle.GetType());
                    g.DrawImage(obstacleImg, obstacle.Location.x, obstacle.Location.y, obstacle.GetSize().Width, obstacle.GetSize().Height);
                    g.DrawRectangle(Pens.Red, obstacle.Location.x, obstacle.Location.y, obstacle.GetSize().Width, obstacle.GetSize().Height);

                }
            }

            return bmp;
        }
    }
}
