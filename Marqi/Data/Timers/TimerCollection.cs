using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marqi.Data.Timers
{
    public class TimerCollection : IDataSource<List<MarqiTimer>>
    {
        private readonly IDictionary<string, MarqiTimer> _timers = new Dictionary<string, MarqiTimer>();

        public void CreateTimer(string name, TimeSpan time)
        {
            _timers[name] = new MarqiTimer
            {
                Name = name,
                Start = DateTime.Now,
                End = DateTime.Now + time,
                Duration = time
            };
        }

        public void CancelTimer(string name)
        {
            _timers.Remove(name);
        }

        public Action<List<MarqiTimer>> Update { get; set; }

        public Task Refresh()
        {
            Update(_timers.Values.ToList());
            return Task.CompletedTask;
        }
    }
}