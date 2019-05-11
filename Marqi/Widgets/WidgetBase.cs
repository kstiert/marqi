using Marqi.Display;

namespace Marqi.Widgets
{
    public abstract class WidgetBase : IWidget
    {
        private bool _dirty;

        public Position Position { get; set; }

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
