using System.Diagnostics;
using System;

namespace TarkovServerU19.BSGClasses
{
    public static class TimeHandler
    {
        public static DateTime UtcNow
        {
            get
            {
                return TimeHandler.dateTime_1.AddMilliseconds((double)TimeHandler.stopwatch_0.ElapsedMilliseconds);
            }
        }

        public static DateTime Now
        {
            get
            {
                return TimeHandler.dateTime_0.AddMilliseconds((double)TimeHandler.stopwatch_0.ElapsedMilliseconds);
            }
        }

        public static double UtcNowUnix
        {
            get
            {
                return TimeHandler.UtcNow.ToUnixTime();
            }
        }

        public static int UtcNowUnixInt
        {
            get
            {
                return (int)TimeHandler.UtcNow.ToUnixTime();
            }
        }

        public static double NowUnix
        {
            get
            {
                return TimeHandler.Now.ToUnixTime();
            }
        }

        public static DateTime MoscowNow
        {
            get
            {
                return TimeHandler.UtcNow.AddHours(3.0);
            }
        }

        public static DateTime LocalDateTimeFromUnixTime(double milliseconds)
        {
            return TimeHandler.UniversalDateTimeFromUnixTime(milliseconds).ToLocalTime();
        }

        public static DateTime UniversalDateTimeFromUnixTime(double seconds)
        {
            return TimeHandler.UnixEpoch.AddMilliseconds(seconds * 1000.0);
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return (dateTime - TimeHandler.UnixEpoch).TotalSeconds;
        }

        public static void SetServerTime(double milliseconds)
        {
            TimeHandler.stopwatch_0 = Stopwatch.StartNew();
            TimeHandler.dateTime_1 = TimeHandler.UniversalDateTimeFromUnixTime(milliseconds);
            TimeHandler.dateTime_0 = TimeHandler.dateTime_1.ToLocalTime();
        }

        public static DateTime FromHour(int hour)
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, hour, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime StartOfDay()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
        }

        public const int SECONDS_IN_MINUTE = 60;
        public const int SECONDS_IN_HOUR = 3600;
        public const int SECONDS_IN_DAY = 86400;
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private static DateTime dateTime_0 = DateTime.Now;
        private static DateTime dateTime_1 = DateTime.UtcNow;
        private static Stopwatch stopwatch_0 = Stopwatch.StartNew();
    }
}