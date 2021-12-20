using Marqi.Display;
using Marqi.RGB;

namespace Marqi.Widgets
{
    public class ProgressBarWidget : WidgetBase
    {
        public int X { get; set;}

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public float Progress { get; set; } = 0f;

        public Color Color { get; set;}

        public override void Render(IDisplay display)
        {
            display.Fill(X, Y, X + (int)(Progress * Width) - 1, Y + Height - 1, Color);
        }
    }
}