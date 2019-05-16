using Marqi.Display;
using System.Collections.Generic;

namespace Marqi.RGB
{
    public class RGBDisplay : DisplayBase
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

        public override void Clear()
        {
            _canvas.Clear();
        }

        public override void Fill(Color color)
        {
            _canvas.Fill(color);
        }

        public override void SetPixel(int x, int y, Color color)
        {
            _canvas.SetPixel(x, y, color);
        }

        public override Font LoadFont(string file)
        {
            _fonts.Add(new RGBLedFont(file));
            return new Font(_fonts.Count - 1);
        }

        public override void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            _canvas.DrawLine(x0, y0, x1, y1, color);
        }

        public override void DrawText(Font font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false)
        {
            _canvas.DrawText(_fonts[font.Id], x, y, color, text, spacing, vertical);
        }

        public override void Swap()
        {
            _matrix.SwapOnVsync(_canvas);
        }
            
    }
}
