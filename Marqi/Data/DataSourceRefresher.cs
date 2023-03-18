using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Marqi.Data
{
    public class DataSourceRefreser : IHostedService
    {
        private readonly ILogger _log;

        private readonly IDictionary<int, Timer> _timers = new ConcurrentDictionary<int, Timer>();

        private readonly IEnumerable<IDataSource> _dataSources;

        private readonly TimeZoneInfo _timeZoneInfo;

        public DataSourceRefreser(ILogger<DataSourceRefreser> logger, IServiceProvider services)
        {
            _dataSources = Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(t => t.GetInterfaces().Contains(typeof(IDataSource)))
                            .Select(t => (IDataSource)services.GetService(t))
                            .Where(source => source != null)
                            .ToList();
            _log = logger;
            _log.LogTrace($"Discovered {_dataSources.Count()} scheduled datasources");

            _timeZoneInfo = TimeZoneInfo.Local;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach(var source in _dataSources)
            {
                if(!string.IsNullOrEmpty(source.Cron))
                {
                    await ScheduleJob(source, cancellationToken);
                }
            }
        }

        private async Task ScheduleJob(IDataSource source, CancellationToken cancellationToken)
        {
            var cron = CronExpression.Parse(source.Cron);
            var next = cron.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            var delay = next.Value - DateTimeOffset.Now;

            if (delay.TotalMilliseconds <= 0)
            {
                await ScheduleJob(source, cancellationToken);
                return;
            }
            var id = Random.Shared.Next();

            _timers[id] = new Timer(async s => {
                var src = (IDataSource)s;
                await src.Refresh();
                await ScheduleJob(src, cancellationToken);
                _timers[id].Dispose();
                _timers.Remove(id);
            }, source, (int)delay.TotalMilliseconds, Timeout.Infinite);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach(var t in _timers.Values)
            {
                t.Dispose();
            }
            return Task.CompletedTask;
        }
    }
}