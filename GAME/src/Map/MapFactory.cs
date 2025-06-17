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
            Image mapImage = CreateMapImage(obstacles);

            return (map, mapImage);
        }

        // 최초 1회만 호출되는 함수
        public static Image CreateMapImage(List<Obstacle> obstacles)
        {
            Bitmap bmp = new Bitmap(1500, 1000);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // 장애물만 그리기
                foreach (var obstacle in obstacles)
                {
                    Image obstacleImg = ObstacleManager.CreateImageFromType(obstacle.GetType());
                    g.DrawImage(obstacleImg, obstacle.Location.x, obstacle.Location.y, obstacle.GetSize().Width, obstacle.GetSize().Height);
                }
            }

            return bmp;
        }
    }
}
