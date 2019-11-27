using System;

namespace Logger.Infrastructure
{
    public interface IScheduleHelper
    {
        DateTime GetNextWorkingDay(DateTime current);
        bool IsWorkingDay(DayOfWeek self);
    }
}
