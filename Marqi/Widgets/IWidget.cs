using Marqi.Display;

namespace Marqi.Widgets
{
    public interface IWidget
    {
        Position Position { get; set; }

        bool Update();

        void Render(IDisplay display);
    }
}
