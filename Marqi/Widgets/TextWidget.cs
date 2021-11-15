using Marqi.Display;
using Marqi.RGB;

namespace Marqi.Widgets
{
    public class TextWidget : WidgetBase
    {
        public Color Color { get; set; }

        public Font Font { get; set; }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                Dirty();
            }
        }

        public override void Render(IDisplay display)
        {
            if(!string.IsNullOrEmpty(Text))
            {
                display.DrawText(Font, Position.X, Position.Y, Color, Text);
            }
        }
    }
}
