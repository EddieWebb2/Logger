using System;
using System.Configuration;
using Logger.Types;

namespace Logger.Infrastructure
{
    public class LoggerConfiguration: ILoggerConfiguration
    {
        public ReleaseModes Mode { get; set; }

        public LoggerConfiguration()
        {
            // I will replace this with my own custom app config handler
            Mode = (ReleaseModes)Enum.Parse(typeof(ReleaseModes), ConfigurationManager.AppSettings["Mode"]);
        }

    }
}
