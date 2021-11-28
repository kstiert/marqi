using System;
using Marqi.Fonts;
using Marqi.RGB;
using Orvid.Graphics.FontSupport.bdf;

namespace Marqi.Display
{
    public abstract class GenericDisplayBase : IDisplay
    {
        private readonly IFontFactory<BDFFontContainer> _fontFactory;

        private readonly IGenericCanvas _canvas;

        protected GenericDisplayBase(IFontFactory<BDFFontContainer> fontFactory, IGenericCanvas canvas)
        {
            _fontFactory = fontFactory;
            _canvas = canvas;
        }

        public virtual void Clear()
        {
            _canvas.Clear();
        }

        public virtual void Fill(Color color)
        {
            _canvas.Fill(color);
        }

        public virtual void SetPixel(int x, int y, Color color)
        {
            _canvas.SetPixel(x, y, color);
        }

        public virtual void Swap()
        {
            _canvas.Swap();
        }

        public void DrawLine(int x0, int y0, int x1, int y1, RGB.Color color)
        {
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if(x0 > x1)
                {
                    DrawLineLow(x1, y1, x0, y0, color);
                }
                else
                {
                    DrawLineLow(x0, y0, x1, y1, color);
                }
            }
            else
            {
                if (y0 > y1)
                {
                    DrawLineHigh(x1, y1, x0, y0, color);
                }
                else
                {
                    DrawLineHigh(x0, y0, x1, y1, color);
                }
            }
        }

        public void DrawText(Font font, int x, int y, RGB.Color color, string text, int spacing = 0, bool vertical = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var bdf = _fontFactory.GetFont(font.Id);
            var offset = 0;
            foreach (var c in text)
            {
                var glyph = bdf.getGlyph(c);
                var glyphBox = glyph.getBbx();
                var fHeight = glyphBox.height;
                var fData = glyph.getData();
                var scan = fData.Length / fHeight;
                var bx = x + offset + glyphBox.x;
                var by = y - fHeight - glyphBox.y;

                for (int k = 0; k < fHeight; k++)
                {
                    var offsetLine = k * scan;
                    for (int j = 0; j < scan; j++)
                    {
                        var fPixel = fData[offsetLine + j];
                        if (fPixel != 0)
                        {
                            SetPixel(bx + j, by + k, color);
                        }
                    }
                }
                offset += glyph.getDWidth().width;
            }
        }

        private void DrawLineLow(int x0, int y0, int x1, int y1, RGB.Color color)
        {
            var dx = x1 - x0;
            var dy = y1 - y0;
            var yi = 1;
            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            int D = 2 * dy - dx;
            int y = y0;

            for(int x = x0; x < x1; x++)
            {
                SetPixel(x, y, color);
                if(D > 0)
                {
                    y += yi;
                    D -= 2 * dx;
                }
                D += 2 * dy;
            }
        }

        private void DrawLineHigh(int x0, int y0, int x1, int y1, RGB.Color color)
        {
            var dx = x1 - x0;
            var dy = y1 - y0;
            var xi = 1;
            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }
            int D = 2 * dx - dy;
            int x = x0;

            for (int y = y0; y < y1; y++)
            {
                SetPixel(x, y, color);
                if (D > 0)
                {
                    x += xi;
                    D -= 2 * dy;
                }
                D += 2 * dx;
            }
        }
    }
}
