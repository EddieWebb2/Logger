using System;
using Logger.Infrastructure;
using Logger.Models;
using Serilog;

namespace Logger.Schedules
{
    public class WeeklySchedule : IScheduler
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<WeeklySchedule>();

        private readonly IScheduleHelper _scheduleHelper;
        public AlertFrequency ScheduleType { get; }
        public DateTime NextRunDate { get; private set; }

        public void SetNextRunDate(ILoggerConfiguration config, DateTime current)
        {
            if (current.TimeOfDay >= config.Weekly)
            {
                current = _scheduleHelper.GetNextWorkingDay(current);
            }

            while (current.DayOfWeek != config.WeeklyDay)
            {
                current = _scheduleHelper.GetNextWorkingDay(current);
            }

            NextRunDate = current.Date + config.Weekly;
        }

        public WeeklySchedule(IScheduleHelper helper)
        {
            _scheduleHelper = helper;
            ScheduleType = AlertFrequency.Weekly;
        }
    }
}
