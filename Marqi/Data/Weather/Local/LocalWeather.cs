using Iot.Device.Sht4x;
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Threading.Tasks;

namespace Marqi.Data.Weather.LocalWeather
{
    public class LocalWeather : IDataSource<List<WeatherReport>>
    {
        private readonly Sht4x _sht4x;

        public LocalWeather()
        {
            var conn = new I2cConnectionSettings(1, Sht4x.DefaultI2cAddress);
            var device = I2cDevice.Create(conn);
            _sht4x = new Sht4x(device);
        }

        public Action<List<WeatherReport>> Update { get; set; }

        public string Cron => "*/5 * * * *";

        public async Task Refresh()
        {
            var reading = await _sht4x.ReadHumidityAndTemperatureAsync();
            
            Update(new List<WeatherReport> 
            { 
                new WeatherReport 
                {
                    Temperature = reading.Temperature?.DegreesFahrenheit.ToString() ?? "??",
                    Humidity = reading.RelativeHumidity?.Percent.ToString() ?? "??",
                    Location = "Home"
                }
            });
        }
    }
}