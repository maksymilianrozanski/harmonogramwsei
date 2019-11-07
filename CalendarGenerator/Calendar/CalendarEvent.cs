using System;
using System.Threading;

namespace CalendarGenerator.Calendar
{
    public class CalendarEvent
    {
        public static long LastUidTime;
        private static readonly object Lock = new object();
        public string Description;
        public string Location;
        public string Summary;
        public DateTime TimeStamp;
        public string Uid;

        public CalendarEvent()
        {
            TimeStamp = DateTime.Now;
            var thread = Thread.CurrentThread;
            Uid = thread.ManagedThreadId + "@" + UniqueUidTime();
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        private static long UniqueUidTime()
        {
            lock (Lock)
            {
                var currentTimeInMillis = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000;
                if (currentTimeInMillis < LastUidTime) currentTimeInMillis = LastUidTime;
                if (currentTimeInMillis - LastUidTime < 1) currentTimeInMillis++;
                LastUidTime = currentTimeInMillis;
                return LastUidTime;
            }
        }
    }
}