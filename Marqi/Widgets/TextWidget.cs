using Marqi.Display;
using Marqi.RGB;

namespace Marqi.Widgets
{
    public class TextWidget : WidgetBase
    {
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
            display.DrawText(Font, Position.X, Position.Y, new Color(255, 0, 0), Text);
        }
    }
}
