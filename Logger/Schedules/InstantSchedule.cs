﻿using System;
using Logger.Infrastructure;
using Logger.Types;

namespace Logger.Schedules
{
    public class InstantSchedule : IScheduler
    {
        private readonly ScheduleHelper _scheduleHelper;

        public AlertFrequencies AlertFrequency => AlertFrequencies.Instant;

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
                current = _scheduleHelper.GetNextWorkingDay(current);

            if (current.TimeOfDay < config.DayStart) current = current.Date + config.DayStart;

            NextRunDate = current;
        }

        public InstantSchedule(ScheduleHelper helper)
        {
            _scheduleHelper = helper;

            var validator = new ScheduleValidator(this);
            validator.Validate();
        }
    }
}