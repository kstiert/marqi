using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Marqi.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Marqi.Data.GCalendar
{
    public class GoogleCalendar : IDataSource<List<GoogleCalendarEvent>>
    {
        private readonly ILogger _logger;

        private readonly IHttpClientFactory _httpFactory;

        private readonly GoogleCalendarOptions _options;

        public GoogleCalendar(ILogger<GoogleCalendar> logger, IHttpClientFactory httpFactory, IOptions<GoogleCalendarOptions> options)
        {
            _logger = logger;
            _httpFactory = httpFactory;
            _options = options.Value;
        }

        public string Cron => "*/20 * * * *";

        public Action<List<GoogleCalendarEvent>> Update { get; set; }

        public async Task Refresh()
        {
            _logger.LogDebug("Refreshing google calendar");

            try
            {
                if(_options.Calendars == null || !_options.Calendars.Any())
                {
                    _logger.LogWarning("No Urls configured for google calendar");
                    return;
                }
                
                using (var client = _httpFactory.CreateClient())
                {
                    var tasks = _options.Calendars.Select(GetCalendarEvents);
                    var events = (await Task.WhenAll(tasks))
                        .SelectMany(gce => gce)
                        .OrderBy(e => e.UtcTime)
                        .ToList();
                    Update(events);
                }

                _logger.LogDebug("Completed refreshing google calendar");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed refreshing google calendar");
            }
        }

        private GoogleCalendarEvent MakeEvent(Occurrence o, string hexColor)
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

            Color color = null;

            if (!string.IsNullOrEmpty(hexColor))
            {
                color = hexColor.ColorFromHex();
            }

            return new GoogleCalendarEvent { Name = ev.Summary, Start = datetime, Color = color, UtcTime = o.Period.StartTime.Value};
        }

        private async Task<List<GoogleCalendarEvent>> GetCalendarEvents(GoogleCalendarConfig config)
        {
            var calResp = await _httpFactory.CreateClient().GetAsync(config.Url);
            var cal = Calendar.Load(await calResp.Content.ReadAsStreamAsync());

            return cal.GetOccurrences(DateTime.Today, DateTime.Today.AddDays(14))
            .Select(o => MakeEvent(o, config.HexColor)).ToList();

        }
    }
}
