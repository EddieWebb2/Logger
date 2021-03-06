﻿using System;
using Logger.Infrastructure;
using Serilog;

namespace Logger.Schedules
{
    public class InstantSchedule : IScheduler
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<InstantSchedule>();

        private readonly IScheduleHelper _scheduleHelper;
        public string ScheduleType { get; }
        public DateTime NextRunDate { get; private set; }

        public void SetNextRunDate(ILoggerConfiguration config, DateTime current)
        {
            current = current.Add(config.InstantInterval);

            if (current.TimeOfDay > config.DayEnd)
            {
                current = _scheduleHelper.GetNextWorkingDay(current);
                current = current.Date + config.DayStart;
            }
            else if (!_scheduleHelper.IsWorkingDay(current.DayOfWeek))
            {
                current = _scheduleHelper.GetNextWorkingDay(current);
            }


            if (current.TimeOfDay < config.DayStart)
            {
                current = current.Date + config.DayStart;
            }

            NextRunDate = current;
        }

        public InstantSchedule(IScheduleHelper helper)
        {
            _scheduleHelper = helper;
            ScheduleType = "Instant";
        }
    }
}
