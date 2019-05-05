using Marqi.Display;
using Marqi.RGB;
using System;
using System.Collections.Generic;
using System.Text;

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
            display.DrawText(Font, 0, 15, new Color(255, 0, 0), Text);
        }
    }
}
