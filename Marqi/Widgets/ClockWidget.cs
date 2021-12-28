using Marqi.Display;
using Marqi.Fonts;
using Marqi.RGB;
using System;

namespace Marqi.Widgets
{
    public class ClockWidget : WidgetBase
    {
        public Color Color { get; set; }

        public Font Font { get; set; }

        public override void Render(IDisplay display)
        {
            display.DrawText(Font, Position.X, Position.Y, Color, DateTime.Now.ToString("h:mm").PadLeft(5));
        }
    }
}