using System;
using Logger.Infrastructure;

namespace Logger.Schedules
{
    public class ScheduleHelper: IScheduleHelper
    {
        private readonly ILoggerConfiguration _config;

        public ScheduleHelper(ILoggerConfiguration config)
        {
            _config = config;
        }

        public DateTime GetNextWorkingDay(DateTime current)
        {
            if (!_config.RunAtWeekend)
                switch (current.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        current = current.AddDays(3);
                        break;
                    case DayOfWeek.Saturday:
                        current = current.AddDays(2);
                        break;
                    default:
                        current = current.AddDays(1);
                        break;
                }
            else
                current = current.AddDays(1);

            return current;
        }

        public bool IsWorkingDay(DayOfWeek self)
        {
            return _config.RunAtWeekend || !(self == DayOfWeek.Saturday || self == DayOfWeek.Sunday);
        }
    }
}
