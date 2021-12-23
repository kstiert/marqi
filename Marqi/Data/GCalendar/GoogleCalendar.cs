using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Marqi.Data.GCalendar
{
    public class GoogleCalendar : IDataSource<List<GoogleCalendarEvent>>
    {
        private readonly ILogger _logger;

        private readonly GoogleCalendarOptions _options;

        public GoogleCalendar(ILogger<GoogleCalendar> logger, IOptions<GoogleCalendarOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public string Cron => "*/20 * * * *";

        public Action<List<GoogleCalendarEvent>> Update { get; set; }

        public async Task Refresh()
        {
            _logger.LogDebug("Refreshing google calendar");

            try
            {
                var calResp = await new HttpClient().GetAsync(_options.Url);
                var cal = Calendar.Load(await calResp.Content.ReadAsStreamAsync());
                var events = cal.GetOccurrences(DateTime.Today, DateTime.Today.AddDays(14))
                                .OrderBy(o => o.Period.StartTime)
                                .Select(MakeEvent).ToList();

                Update(events);
                _logger.LogDebug("Completed refreshing google calendar");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed refreshing google calendar");
            }
        }

        public GoogleCalendarEvent MakeEvent(Occurrence o)
        {
            var ev = o.Source as CalendarEvent;
            var start = o.Period.StartTime.AsSystemLocal;
            var end = o.Period.EndTime.AsSystemLocal;
            var datetime = start.ToString("M/d");
            if (ev.IsAllDay)
            {
                if (ev.Duration > TimeSpan.FromDays(1))
                {
                    datetime += $"-{end.AddDays(-1).ToString("M/d")}";
                }
            }
            else
            {
                datetime += $" {start.ToString("h:mmt")}";
            }

            return new GoogleCalendarEvent { Name = ev.Summary, Start = datetime };
        }
    }
}
