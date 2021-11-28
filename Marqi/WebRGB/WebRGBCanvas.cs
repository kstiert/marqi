using System.IO;
using System.Threading.Tasks;
using Marqi.Options;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Marqi.WebRGB
{
    public class WebRGBCanvas : IWebRGBCanvas
    {
        private Image<Rgba32> _screen;
        private Image<Rgba32> _buffer;

        private readonly DisplayOptions _displayOptions;

        public WebRGBCanvas(IOptions<DisplayOptions> options)
        {
            _displayOptions = options.Value;
            _screen = new Image<Rgba32>(Width, Height, Color.Black);
            _buffer = new Image<Rgba32>(Width, Height, Color.Black);
        }

        public int Width { get { return _displayOptions.Columns * _displayOptions.PixelSize; } }

        public int Height { get { return _displayOptions.Rows * _displayOptions.PixelSize; } }

        public void Swap()
        {
            var t = _buffer;
            _buffer = _screen;
            _screen = t;
        }

        public void SetPixel(int x, int y, RGB.Color color)
        {
            if(x >= _displayOptions.Columns || y >= _displayOptions.Rows || x < 0 || y < 0)
            {
                return;
            }
            var halfSize = _displayOptions.PixelSize / 2;
            var pixel = new EllipsePolygon(new PointF(x * _displayOptions.PixelSize + halfSize, y * _displayOptions.PixelSize + halfSize), new SizeF(halfSize, halfSize));
            _buffer.Mutate(c => c.Fill(Color.FromRgb(color.R, color.G, color.B), pixel));
        }

        public async Task<Stream> GetScreenStream()
        {
            var stream = new MemoryStream();
            await _screen.SaveAsPngAsync(stream);
            stream.Position = 0;
            return stream;
        }

        public void Clear()
        {
            _buffer.Mutate(i => i.Fill(Color.Black));
        }

        public void Fill(RGB.Color color)
        {
            _buffer.Mutate(i => i.Fill(Color.FromRgb(color.R, color.G, color.B)));
        }
    }
}