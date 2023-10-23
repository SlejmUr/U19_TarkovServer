using System.Diagnostics;

namespace TarkovServerU19.BSGClasses
{
    public static class StopwatchExtensions
    {
        public static float ElapsedSeconds(this Stopwatch stopwatch)
        {
            return (float)stopwatch.ElapsedMilliseconds / 1000f;
        }

        public static double ElapsedMillisecondsAsDouble(this Stopwatch stopwatch)
        {
            return (double)stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0;
        }
    }
}