using System;
using Logger.Models;

namespace Logger.Infrastructure
{
    public interface IScheduler
    {
        AlertFrequency ScheduleType { get; }
        DateTime NextRunDate { get; }
        void SetNextRunDate(ILoggerConfiguration config, DateTime current);
    }
}
