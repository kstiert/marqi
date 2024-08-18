using System;
using Marqi.Common;

namespace Marqi.Data.GCalendar
{
    public class GoogleCalendarEvent
    {
        public string Start { get; set; }

        public string Name { get; set; }

        public Color Color { get; set; }

        public DateTime UtcTime { get; set; }
    }
}
