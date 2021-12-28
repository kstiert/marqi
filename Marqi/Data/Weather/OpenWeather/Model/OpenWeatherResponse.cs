namespace Marqi.Data.Weather.OpenWeather.Model
{
    public class OpenWeatherResponse
    {
        public string name { get; set;}

        public MainWeather main { get; set; }
    }
}