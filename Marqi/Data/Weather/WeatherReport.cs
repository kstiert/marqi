using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Marqi.Data.Weather
{
    public class WeatherReport
    {
        public string Location { get; set; }

        public string Temperature { get; set; }

        public string Humidity { get; set; }

        public Image<Rgba32> Icon { get; set; }
    }
}