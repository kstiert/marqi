using System;

namespace Marqi.Data.Timers
{
    public class MarqiTimer
    {
        public string Name { get; set; }

        public DateTime Start {get; set;}

        public DateTime End { get; set; }

        public TimeSpan Duration { get; set; }

        public double Progress
        {
            get
            {
                var elapsed = DateTime.Now - Start;
                return Math.Min(elapsed/Duration, 1);
            }
        }

    }
}