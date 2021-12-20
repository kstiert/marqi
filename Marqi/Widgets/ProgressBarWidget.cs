using Marqi.Display;
using Marqi.RGB;

namespace Marqi.Widgets
{
    public class ProgressBarWidget : WidgetBase
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public double Progress { get; set; } = 0f;

        public Color Color { get; set;}

        public override void Render(IDisplay display)
        {
            display.Fill(Position.X, Position.Y, Position.X + (int)(Progress * Width) - 1, Position.Y + Height - 1, Color);
        }
    }
}