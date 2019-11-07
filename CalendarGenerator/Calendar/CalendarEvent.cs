using System;
using System.Threading;

namespace CalendarGenerator.Calendar
{
    public class CalendarEvent
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime TimeStamp;
        public string Uid;
        public string Summary;
        public string Description;
        public string Location;
        public static long LastUidTime = 0;
        private static readonly object Lock = new object();

        public CalendarEvent()
        {
            TimeStamp = DateTime.Now;
            Thread thread = Thread.CurrentThread;
            Uid = thread.ManagedThreadId + "@" + UniqueUidTime();
        }

        private static long UniqueUidTime()
        {
            lock (Lock)
            {
                long currentTimeInMillis = DateTime.Now.Millisecond;
                if (currentTimeInMillis < LastUidTime)
                {
                    currentTimeInMillis = LastUidTime;
                }

                if (currentTimeInMillis - LastUidTime < 1)
                {
                    currentTimeInMillis++;
                }

                LastUidTime = currentTimeInMillis;
                return LastUidTime;
            }
        }
    }
}