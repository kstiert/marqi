using Marqi.RGB;

namespace Marqi.Display
{
    public static class DisplayExtensions
    {
        public static void Fill(this IDisplay display, int x1, int y1, int x2, int y2, Color c)
        {
            for(int x = x1; x <= x2; x++)
            {
                for(int y = y1; y <= y2; y++)
                {
                    display.SetPixel(x, y, c);
                }
            }
        }
    }
}