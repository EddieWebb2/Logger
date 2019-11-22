using System;
using Logger.Infrastructure;
using Logger.Types;

namespace Logger.Schedules
{
    public class DailySchedule : IScheduler
    {
        private readonly IScheduleHelper _scheduleHelper;

        public AlertFrequencies AlertFrequency => AlertFrequencies.Daily;

        public DateTime NextRunDate { get; private set; }

        public void SetNextRunDate(ILoggerConfiguration config, DateTime current)
        {
            if (current.TimeOfDay >= config.Daily || !_scheduleHelper.IsWorkingDay(current.DayOfWeek))
                current = _scheduleHelper.GetNextWorkingDay(current);

            NextRunDate = current.Date + config.Daily;
        }

        public DailySchedule(IScheduleHelper scheduleHelper)
        {
            _scheduleHelper = scheduleHelper;

            var validator = new ScheduleValidator(this);
            validator.Validate();
        }
    }
}