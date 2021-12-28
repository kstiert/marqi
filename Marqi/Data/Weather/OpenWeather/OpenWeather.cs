using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Marqi.Data.Weather.OpenWeather.Model;
using Microsoft.Extensions.Options;

namespace Marqi.Data.Weather.OpenWeather
{
    public class OpenWeather : IDataSource<List<WeatherReport>>
    {
        private readonly IHttpClientFactory _httpFactory;

        private readonly string _apiKey;

        private readonly List<string> _zips;

        public OpenWeather(IHttpClientFactory httpFactory, IOptions<OpenWeatherOptions> options)
        {
            _httpFactory = httpFactory;
            _apiKey = options.Value.ApiKey;
            _zips = options.Value.Zip;
        }

        public Action<List<WeatherReport>> Update { get; set; }

        public string Cron => "*/5 * * * *";

        public async Task Refresh()
        {
            var resp = await _httpFactory.CreateClient().GetAsync($"http://api.openweathermap.org/data/2.5/weather?zip={_zips[0]}&units=imperial&appid={_apiKey}");
            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var weather = JsonSerializer.Deserialize<OpenWeatherResponse>(content);
                Update(new List<WeatherReport> { new WeatherReport {
                    Temperature = weather.main.temp.ToString("F0"),
                    Location = weather.name
                }});
            }
        }
    }
}