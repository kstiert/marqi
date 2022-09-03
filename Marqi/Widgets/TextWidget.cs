using Marqi.Common.Display;
using Marqi.Common.Fonts;
using Marqi.Common;

namespace Marqi.Widgets
{
    public class TextWidget : WidgetBase
    {
        public Color Color { get; set; }

        public Font Font { get; set; }

        public int? Truncate { get; set; }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if(Truncate.HasValue)
                {
                    _text = value.Substring(0, Truncate.Value);
                }
                else
                {
                    _text = value;
                }
                               
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
