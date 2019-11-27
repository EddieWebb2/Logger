using System;

namespace Logger.Infrastructure
{
    public interface ILoggerConfiguration
    {
        string SoftwareName { get; }

        TimeSpan InstantInterval { get; }
        TimeSpan DayStart { get; }
        TimeSpan DayEnd { get; }
        TimeSpan Daily { get; }
        TimeSpan Weekly { get; }
        DayOfWeek WeeklyDay { get; }
        bool RunAtWeekend { get; }
    }
}
