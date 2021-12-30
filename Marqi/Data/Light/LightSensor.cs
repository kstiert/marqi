using Iot.Device.Bh1750fvi;
using System;
using System.Device.I2c;
using System.Threading.Tasks;

namespace Marqi.Data.Light
{
    public class LightSensor : IDataSource<LightLevel>
    {
        private readonly Bh1750fvi _bh1750;

        public LightSensor()
        {
            var conn = new I2cConnectionSettings(1, (int)I2cAddress.AddPinLow);
            var device = I2cDevice.Create(conn);
            _bh1750 = new Bh1750fvi(device);
        }

        public Action<LightLevel> Update { get; set; }

        public string Cron => "* * * * *";

        public Task Refresh()
        {
            Update(new LightLevel { Lux = _bh1750.Illuminance.Kilolux });
            return Task.CompletedTask;
        }
    }
}