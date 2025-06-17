using System;
using System.Drawing;

namespace Game.Obstacles
{
    public static class ObstacleManager
    {
        public static Obstacle CreateObstacleFromType(Type type)
        {
            return (Obstacle)Activator.CreateInstance(type);
        }

        public static Image CreateImageFromType(Type type)
        {
            return CreateObstacleFromType(type).GetImage();
        }

        public static Size GetObstacleSize(Type type)
        {
            return CreateObstacleFromType(type).GetSize();
        }
    }
}
