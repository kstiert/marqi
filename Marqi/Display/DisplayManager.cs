using Marqi.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marqi.Display
{
    public class DisplayManager
    {
        private readonly IList<IWidget> _widgets = new List<IWidget>();

        private readonly IEnumerable<IDisplay> _displays;

        public void Update()
        {
            var render = false;
            foreach (var widget in _widgets)
            {
                if (widget.Update())
                {
                    render = true;
                }
            }
            
            if (render)
            {
                foreach (var widget in _widgets)
                {
                    foreach(var display in _displays)
                    {
                        widget.Render(display);
                    }
                }

                foreach(var display in _displays)
                {
                    display.Swap();
                    display.Clear();
                }

            }
        }
    }
}
