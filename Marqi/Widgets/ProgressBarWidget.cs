using Marqi.Display;
using Marqi.RGB;

namespace Marqi.Widgets
{
    public class ProgressBarWidget : WidgetBase
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public double Progress { get; set; } = 0.0;

        public Color Color { get; set;}

        public Color CompleteColor {get ;set;}

        public override void Render(IDisplay display)
        {
            var c = Progress >= 1.0 ? CompleteColor : Color;

            display.Fill(Position.X, Position.Y, Position.X + (int)(Progress * Width) - 1, Position.Y + Height - 1, c);
        }
    }
}