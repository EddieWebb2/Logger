using System;
using Logger.Types;

namespace Logger.Infrastructure
{
    public interface ILoggerConfiguration
    {
        ReleaseModes Mode { get; }

        TimeSpan InstantInterval { get; }
        TimeSpan DayStart { get; }
        TimeSpan DayEnd { get; }
        TimeSpan Daily { get; }
        TimeSpan Weekly { get; }
        DayOfWeek WeeklyDay { get; }
        bool RunAtWeekend { get; }
    }
}
