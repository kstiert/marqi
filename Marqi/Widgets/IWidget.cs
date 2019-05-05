using Marqi.Display;

namespace Marqi.Widgets
{
    public interface IWidget
    {
        bool Update();

        void Render(IDisplay display);
    }
}
