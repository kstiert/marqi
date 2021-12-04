using Marqi.RGB;

namespace Marqi.Display
{
    public interface ICanvas
    {
        void SetPixel(int x, int y, Color color);

        void Fill(Color color);

        void Clear();
    }
}