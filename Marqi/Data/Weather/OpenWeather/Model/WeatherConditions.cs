namespace Marqi.Data.Weather.OpenWeather.Model
{
    public class WeatherConditions
    {
        public int id { get; set; }

        public string main { get; set; }

        public string description { get; set; }

        public string icon { get; set; }
    }
}