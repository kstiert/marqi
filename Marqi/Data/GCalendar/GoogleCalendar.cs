using System;
using System.Linq;
using System.Net.Http;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace Marqi.Data.GCalendar
{
    public class GoogleCalendar : IDataSource<GoogleCalendarEvent>
    {
        public Action<GoogleCalendarEvent> Update { get; set; }

        public async void Refresh()
        {
            var calResp = await new HttpClient().GetAsync("https://calendar.google.com/calendar/ical/j822eaksjlh7ktnemt509pu95k%40group.calendar.google.com/private-2fcb409c2dd5a289303adff532f42a4f/basic.ics");
            var cal = Calendar.Load(calResp.Content.ReadAsStreamAsync().Result);
            var ev = cal.GetOccurrences(DateTime.Today, DateTime.Today.AddDays(7)).Select(o => o.Source).Cast<CalendarEvent>().OrderBy(e => e.Start).First();
            var start = ev.Start.Value.ToLocalTime();
            var end = ev.End.Value.ToLocalTime();
            var datetime = start.ToString("M/d");
            if (ev.IsAllDay)
            {
                if(end.Date != start.Date)
                {
                    datetime += $"-{end.ToString("M/d")}";
                }
            }
            else
            {
                datetime += $" {start.ToShortTimeString()}";
            }

            Update(new GoogleCalendarEvent { Name = ev.Summary, Start = datetime });
        }
    }
}
