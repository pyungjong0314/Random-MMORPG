using System.Drawing;

namespace Game.Obstacles
{
    public abstract class Obstacle
    {
        public (int x, int y) Location { get; set; }

        protected virtual Size DefaultSize => new Size(64, 64); // 기본값
        public virtual Size GetSize() => DefaultSize;

        public abstract Image GetImage();
    }

    public class Rock : Obstacle
    {
        protected override Size DefaultSize => new Size(62, 50);
        public override Image GetImage() => WindowsFormsApp1.Properties.Resources.rock;
    }

    public class Tree : Obstacle
    {
        protected override Size DefaultSize => new Size(93, 73);
        public override Image GetImage() => WindowsFormsApp1.Properties.Resources.tree;
    }

    public class Well : Obstacle
    {
        protected override Size DefaultSize => new Size(142, 142);
        public override Image GetImage() => WindowsFormsApp1.Properties.Resources.well;
    }
}
