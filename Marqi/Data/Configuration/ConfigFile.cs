using Newtonsoft.Json;
using System;
using System.IO;

namespace Marqi.Data.Configuration
{
    public class ConfigFile<T> : IDataSource<T>
    {
        private readonly string _file;

        public ConfigFile(string file)
        {
            _file = file;
        }

        public Action<T> Update { get; set; }

        public void Refresh()
        {
            File.OpenRead(_file).;
            JsonConvert.DeserializeObject<T>()
        }
    }
}
