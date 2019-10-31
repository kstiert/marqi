using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Marqi.Data.Configuration
{
    public class ConfigFile<T> : IDataSource<T>
    {
        private readonly string _path;

        public ConfigFile(string path)
        {
            _path = path;
        }

        public Action<T> Update { get; set; }

        public async Task Refresh()
        {
            var stream = new StreamReader(_path);
            var data = JsonConvert.DeserializeObject<T>(await stream.ReadToEndAsync());
        }
    }
}
