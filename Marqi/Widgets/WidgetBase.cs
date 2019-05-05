using Marqi.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marqi.Widgets
{
    public abstract class WidgetBase : IWidget
    {
        private bool _dirty;

        public abstract void Render(IDisplay display);

        public virtual bool Update()
        {
            if(_dirty)
            {
                _dirty = false;
                return true;
            }
            return false;
        }

        protected void Dirty()
        {
            _dirty = true;
        }
    }
}
