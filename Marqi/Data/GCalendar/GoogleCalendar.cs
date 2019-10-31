using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace Marqi.Data.GCalendar
{
    public class GoogleCalendar : IDataSource<List<GoogleCalendarEvent>>
    {
        public Action<List<GoogleCalendarEvent>> Update { get; set; }

        public async Task Refresh()
        {
            var calResp = await new HttpClient().GetAsync("https://calendar.google.com/calendar/ical/j822eaksjlh7ktnemt509pu95k%40group.calendar.google.com/private-2fcb409c2dd5a289303adff532f42a4f/basic.ics");
            var cal = Calendar.Load(await calResp.Content.ReadAsStreamAsync());
            var events = cal.GetOccurrences(DateTime.Today, DateTime.Today.AddDays(14))
                            .OrderBy(o => o.Period.StartTime)
                            .Select(MakeEvent).ToList();
            Update(events);
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
