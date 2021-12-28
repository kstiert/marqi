using System.Collections.Generic;

namespace Marqi.Data.Weather.OpenWeather
{
    public class OpenWeatherOptions
    {
        public string ApiKey { get; set; }

        public List<string> Zip { get; set; }
    }
}