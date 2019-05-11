using System;
using System.Collections.Generic;
using System.Text;
using Marqi.RGB;
using Marqi.Widgets;

namespace Marqi.Display
{
    public abstract class DisplayBase : IDisplay
    {
        protected List<IWidget> Widgets { get; private set; } = new List<IWidget>();

        public virtual void AddWidget(IWidget widget)
        {
            Widgets.Add(widget);
        }

        public virtual void Update()
        {
            var render = false;
            foreach (var widget in Widgets)
            {
                if(widget.Update())
                {
                    render = true;
                }
            }

            if(render)
            {
                foreach (var widget in Widgets)
                {
                    widget.Render(this);
                }
                Swap();
                Clear();
            }
        }

        public abstract void Clear();
        public abstract void DrawText(Font font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false);
        public abstract void Fill(Color color);
        public abstract Font LoadFont(string file);
        public abstract void SetPixel(int x, int y, Color color);
        public abstract void Swap();
    }
}
