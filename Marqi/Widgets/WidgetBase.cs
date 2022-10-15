using System;
using Marqi.Common.Display;

namespace Marqi.Widgets
{
    public abstract class WidgetBase : IWidget
    {
        private bool _dirty;

        public WidgetBase()
        {
            _dirty = true;
        }

        public Position Position { get; set; }

        public abstract void Render(IDisplay display);

        public virtual bool Update(TimeSpan delta)
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
