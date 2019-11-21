using System;
using Logger.Types;

namespace Logger.Infrastructure
{
    public interface IScheduler
    {
        AlertFrequencies AlertFrequency { get;  }
        DateTime NextRunDate { get; }

        void SetNextRunDate(ILoggerConfiguration config, DateTime current);
    }
}
