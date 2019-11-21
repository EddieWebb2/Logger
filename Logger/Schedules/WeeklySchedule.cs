using System;
using Logger.Infrastructure;
using Logger.Types;

namespace Logger.Schedules
{
    public class WeeklySchedule : IScheduler
    {
        private readonly ScheduleHelper _scheduleHelper;

        public AlertFrequencies AlertFrequency => AlertFrequencies.Weekly;

        public DateTime NextRunDate { get; private set; }

        public void SetNextRunDate(ILoggerConfiguration config, DateTime current)
        {
            if (current.TimeOfDay >= config.Weekly) current = _scheduleHelper.GetNextWorkingDay(current);

            while (current.DayOfWeek != config.WeeklyDay) current = _scheduleHelper.GetNextWorkingDay(current);

            NextRunDate = current.Date + config.Weekly;
        }

        public WeeklySchedule(ScheduleHelper helper)
        {
            _scheduleHelper = helper;

            var validator = new ScheduleValidator(this);
            validator.Validate();
        }
    }
}
