using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace Marqi.Data.GCalendar
{
    public class GoogleCalendar : IDataSource<List<GoogleCalendarEvent>>
    {
        public Action<List<GoogleCalendarEvent>> Update { get; set; }

        public async void Refresh()
        {
            var calResp = await new HttpClient().GetAsync("https://calendar.google.com/calendar/ical/j822eaksjlh7ktnemt509pu95k%40group.calendar.google.com/private-2fcb409c2dd5a289303adff532f42a4f/basic.ics");
            var cal = Calendar.Load(calResp.Content.ReadAsStreamAsync().Result);
            var events = cal.GetOccurrences(DateTime.Today, DateTime.Today.AddDays(7))
                            .Select(o => o.Source)
                            .Cast<CalendarEvent>()
                            .OrderBy(e => e.Start)
                            .Select(MakeEvent).ToList();
            Update(events);
        }

        public GoogleCalendarEvent MakeEvent(CalendarEvent ev)
        {
            var start = ev.Start.Value.ToLocalTime();
            var end = ev.End.Value.ToLocalTime();
            var datetime = start.ToString("M/d");
            if (ev.IsAllDay)
            {
                if (ev.Duration > TimeSpan.FromDays(1))
                {
                    datetime += $"-{end.ToString("M/d")}";
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
