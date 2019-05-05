using Marqi.RGB;
using Marqi.Widgets;

namespace Marqi.Display
{
    public interface IDisplay
    {
        void AddWidget(IWidget widget);
        void Clear();
        void DrawText(Font font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false);
        void Fill(Color color);
        Font LoadFont(string file);
        void SetPixel(int x, int y, Color color);
        void Swap();
    }
}