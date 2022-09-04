using SixLabors.ImageSharp.PixelFormats;

namespace Marqi.Common.Display
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

        public static void DrawImage(this IDisplay display, int x, int y, SixLabors.ImageSharp.Image<Rgba32> image)
        {
            for(int i = 0; i < image.Width; i++)
            {
                for(int j = 0; j < image.Height; j++)
                {
                    var c = image[i, j];
                    if(c.R + c.G + c.B > 30)
                    {
                        display.SetPixel(x + i, y + j, new Color { R = c.R, G = c.G, B = c.B });
                    }
                }
            }
        }
    }
}