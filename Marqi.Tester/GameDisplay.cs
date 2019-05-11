using System;
using System.Collections.Generic;
using System.IO;
using Marqi.Display;
using Marqi.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orvid.Graphics.FontSupport.bdf;

namespace Marqi.Tester
{
    public class GameDisplay : DisplayBase
    {
        private const int PIXEL_SIZE = 20;
        private readonly List<BDFFontContainer> _fonts = new List<BDFFontContainer>();
        private readonly int _rows;
        private readonly int _cols;
        private GraphicsDevice _graphics;
        private Texture2D _screen;
        private Texture2D _buffer;

        public GameDisplay(int rows, int cols)
        {         
            _rows = rows;
            _cols = cols;
        }

        public int Width { get { return _cols * PIXEL_SIZE; } }

        public int Height { get { return _rows * PIXEL_SIZE; } }

        public Texture2D Texture { get { return _screen; } }

        public void Load(GraphicsDevice graphics)
        {
            _graphics = graphics;
            _screen = new Texture2D(graphics, Width, Height);
            _buffer = new Texture2D(graphics, Width, Height);
        }

        public override void Clear()
        {
            _buffer = new Texture2D(_graphics, Width, Height);
        }

        public override void DrawText(Font font, int x, int y, RGB.Color color, string text, int spacing = 0, bool vertical = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var bdf = _fonts[font.Id];
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

        public override void Fill(RGB.Color color)
        {
            var data = new Microsoft.Xna.Framework.Color[Width * Height];
            var c = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B);
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = c;
            }
            _buffer.SetData(data);
        }

        public override Font LoadFont(string file)
        {
            var f = File.OpenRead(file);
            _fonts.Add(BDFFontContainer.CreateFont(f));
            return new Font(_fonts.Count - 1);
        }

        public override void SetPixel(int x, int y, RGB.Color color)
        {
            if(x >= _cols || y >= _rows || x < 0 || y < 0)
            {
                return;
            }

            var data = new Microsoft.Xna.Framework.Color[PIXEL_SIZE * PIXEL_SIZE];
            var c = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = c;
            }
            _buffer.SetData(0, new Rectangle(x * PIXEL_SIZE, y * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE), data, 0, PIXEL_SIZE * PIXEL_SIZE);
        }

        public override void Swap()
        {
            var t = _screen;
            _screen = _buffer;
            _buffer = t;
        }
    }
}
