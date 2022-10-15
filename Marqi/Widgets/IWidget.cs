using System;
using Marqi.Common.Display;

namespace Marqi.Widgets
{
    public interface IWidget
    {
        Position Position { get; set; }

        bool Update(TimeSpan delta);

        void Render(IDisplay display);
    }
}
