using System;
using Logger.Infrastructure;

namespace Logger.Schedules
{
    public class DailySchedule : IScheduler
    {
        private readonly IScheduleHelper _scheduleHelper;
        public string ScheduleType { get; }
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
            ScheduleType = "Daily";
        }
    }
}