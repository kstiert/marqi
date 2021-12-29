using Marqi.Display;
using Marqi.Fonts;
using Microsoft.Extensions.Logging;

namespace Marqi.RGB
{
    public class RGBDisplay : IDisplay
    {
        private ILogger _log;

        private readonly IFontFactory<RGBLedFont> _fontFactory;

        private readonly RGBLedMatrix _matrix;
        private readonly RGBLedCanvas _canvas;

        public RGBDisplay(ILogger<RGBDisplay> logger, IFontFactory<RGBLedFont> fontFactory)
        {
            _log = logger;
            _fontFactory = fontFactory;
            _matrix = new RGBLedMatrix(new RGBLedMatrixOptions
            {
                Rows = 32,
                Cols = 64,
                HardwareMapping = "adafruit-hat",
                Brightness = 50
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

        public void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            _canvas.DrawLine(x0, y0, x1, y1, color);
        }

        public void DrawText(Font font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false)
        {
            _canvas.DrawText(_fontFactory.GetFont(font.Id), x, y, color, text, spacing, vertical);
        }

        public void Swap()
        {
            _matrix.SwapOnVsync(_canvas);
        }
            
    }
}
