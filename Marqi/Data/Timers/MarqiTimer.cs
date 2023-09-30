using System;

namespace Marqi.Data.Timers
{
    public class MarqiTimer
    {
        public string Name { get; set; }

        public DateTime Start {get; set;}

        public DateTime End { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan Elapsed { get { return DateTime.Now - Start; } }

        public TimeSpan Remaining { get { return End - DateTime.Now; } }

        public bool DisplayText { get; set; }

        public double Progress
        {
            get
            {
                return Math.Min(Elapsed/Duration, 1);
            }
        }

    }
}