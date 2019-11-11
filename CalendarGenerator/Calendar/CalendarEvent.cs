using System;
using System.Threading;

namespace CalendarGenerator.Calendar
{
    public class CalendarEvent
    {
        private static long _lastUidTime;
        private static readonly object Lock = new object();
        public readonly string Uid;
        public string Description;
        public string Location;
        public string Summary;
        public DateTime TimeStamp;

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
                if (currentTimeInMillis < _lastUidTime) currentTimeInMillis = _lastUidTime;
                if (currentTimeInMillis - _lastUidTime < 1) currentTimeInMillis++;
                _lastUidTime = currentTimeInMillis;
                return _lastUidTime;
            }
        }
    }
}