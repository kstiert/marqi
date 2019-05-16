using System;
using Marqi.Display;
using Marqi.RGB;

namespace Marqi.Widgets
{
    public class LineWidget : WidgetBase
    {
        public int X0 { get; set; }
        public int Y0 { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }

        public Color Color { get; set; }

        public override void Render(IDisplay display)
        {
            display.DrawLine(X0, Y0, X1, Y1, Color);
        }
    }
}
