using System;

namespace Logger.Infrastructure
{
    public interface IScheduler
    {
        string ScheduleType { get;  }
        DateTime NextRunDate { get; }
        void SetNextRunDate(ILoggerConfiguration config, DateTime current);
    }
}
