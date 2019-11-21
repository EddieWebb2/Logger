using System;
using Logger.Infrastructure;
using Logger.Types;

namespace Logger.Schedules
{
    public class ScheduleHelper
    {
        private readonly ILoggerConfiguration _config;

        public ScheduleHelper(ILoggerConfiguration config) => _config = config;

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
                    case DayOfWeek.Sunday:
                        current = current.AddDays(1);
                        break;
                }
            else
                current = current.AddDays(1);

            return current;
        }

        public bool IsWorkingDay(DayOfWeek self) => _config.RunAtWeekend || !(self == DayOfWeek.Saturday || self == DayOfWeek.Sunday);

        public DateTime LastSearchDate(IScheduler schedule)
        {
            var current = schedule.NextRunDate;
            var span = TimeSpan.Zero;

            switch (schedule.AlertFrequency)
            {
                case AlertFrequencies.Instant:
                    span = _config.InstantInterval;
                    break;
                case AlertFrequencies.Daily:
                    if (!_config.RunAtWeekend && current.DayOfWeek == DayOfWeek.Monday)
                        span = TimeSpan.FromDays(3);
                    else
                        span = TimeSpan.FromDays(1);
                    break;
                case AlertFrequencies.Weekly:
                    span = TimeSpan.FromDays(7);
                    break;
            }

            return current.Subtract(span);
        }
    }
}
