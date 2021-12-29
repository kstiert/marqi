using Marqi.Display;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Marqi.Widgets
{
    public class IconWidget : WidgetBase
    {
        public Image<Rgba32> Icon { get; set;}

        public override void Render(IDisplay display)
        {
            if(Icon != null)
            {
                display.DrawImage(Position.X, Position.Y, Icon);
            }
        }
    }
}