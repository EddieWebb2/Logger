using System;
using Logger.Infrastructure;
using Logger.Models;
using Serilog;

namespace Logger.Schedules
{
    public class DailySchedule : IScheduler
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<DailySchedule>();

        private readonly IScheduleHelper _scheduleHelper;
        public AlertFrequency ScheduleType { get; }
        public DateTime NextRunDate { get; private set; }

        public void SetNextRunDate(ILoggerConfiguration config, DateTime current)
        {
            if (current.TimeOfDay >= config.Daily || !_scheduleHelper.IsWorkingDay(current.DayOfWeek))
            {
                current = _scheduleHelper.GetNextWorkingDay(current);
            }

            NextRunDate = current.Date + config.Daily;
        }

        public DailySchedule(IScheduleHelper scheduleHelper)
        {
            _scheduleHelper = scheduleHelper;
            ScheduleType = AlertFrequency.Daily;
        }
    }
}