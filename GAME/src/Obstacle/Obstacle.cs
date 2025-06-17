using System.Drawing;
using System.Runtime.CompilerServices;

namespace Game.Obstacles
{
    public abstract class Obstacle
    {
        public (int x, int y) Location { get; set; }

        private Size size = new Size(64, 64); // 기본 크기

        public void SetSize(int width, int height)
        {
            size = new Size(width, height);
        }

        public virtual Size GetSize() => size;

        public abstract Image GetImage();
    }

    public class Rock : Obstacle
    {
        public override Image GetImage()
        {
            return WindowsFormsApp1.Properties.Resources.rock;
        }
        
      
    }

    public class Tree : Obstacle
    {
        public override Image GetImage()
        {
            return WindowsFormsApp1.Properties.Resources.tree;
        }
    }

    public class Well : Obstacle
    {
        public override Image GetImage()
        {
            return WindowsFormsApp1.Properties.Resources.well;
        }
    }
}
