using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;

namespace Marqi.Data.Timers
{
    public class TimerCollection : IDataSource<List<MarqiTimer>>
    {
        private const string TIMER_PREFIX = "TIMER_";

        private readonly IEasyCachingProvider _cache;
        private bool disposedValue;

        public TimerCollection(IEasyCachingProvider cache)
        {
            _cache = cache;
        }

        public void CreateTimer(string name, TimeSpan time)
        {
            var key = MakeCacheKey(name);

            if(!_cache.Exists(key))
            {
                _cache.Set(key, new MarqiTimer
                    {
                        Name = name,
                        Start = DateTime.Now,
                        End = DateTime.Now + time,
                        Duration = time
                    }, time + TimeSpan.FromDays(1));
            }
        }

        public string Cron => string.Empty;

        public Action<List<MarqiTimer>> Update { get; set; }

        public async Task<List<MarqiTimer>> ListTimers()
        {
            return (await _cache.GetByPrefixAsync<MarqiTimer>(TIMER_PREFIX)).Values.Select(c => c.Value).ToList();
        }

        public void CancelTimer(string name)
        {
            _cache.Remove(MakeCacheKey(name));
        } 

        public async Task Refresh()
        {
            Update(await ListTimers());
        }

        private string MakeCacheKey(string name)
        {
            return $"{TIMER_PREFIX}{name}";
        }
    }
}