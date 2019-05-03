using Marqi.RGB;
using System.Collections.Generic;

namespace Marqi
{
    public class RGBDisplay : IDisplay
    {
        private readonly List<RGBLedFont> _fonts = new List<RGBLedFont>();
        private readonly RGBLedMatrix _matrix;
        private readonly RGBLedCanvas _canvas;

        public RGBDisplay()
        {
            _matrix = new RGBLedMatrix(new RGBLedMatrixOptions
            {
                Rows = 32,
                Cols = 64,
                HardwareMapping = "adafruit-hat"
            });
            _canvas = _matrix.CreateOffscreenCanvas();
        }

        public void Clear()
        {
            _canvas.Clear();
        }

        public void Fill(Color color)
        {
            _canvas.Fill(color);
        }

        public void SetPixel(int x, int y, Color color)
        {
            _canvas.SetPixel(x, y, color);
        }

        public Font LoadFont(string file)
        {
            _fonts.Add(new RGBLedFont(file));
            return new Font(_fonts.Count - 1);
        }

        public void DrawText(Font font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false)
        {
            _canvas.DrawText(_fonts[font.Id], x, y, color, text, spacing, vertical);
        }

        public void Swap()
        {
            _matrix.SwapOnVsync(_canvas);
        }
            
    }
}
