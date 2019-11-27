using System;
using System.Configuration;

namespace Logger.Infrastructure
{
    public class LoggerConfiguration: ILoggerConfiguration
    {
        public string SoftwareName { get; }
        public TimeSpan InstantInterval { get; set; }
        public TimeSpan DayStart { get; set; }
        public TimeSpan DayEnd { get; set; }
        public TimeSpan Daily { get; set; }
        public TimeSpan Weekly { get; set; }
        public DayOfWeek WeeklyDay { get; set; }
        public bool RunAtWeekend { get; set; }

        public LoggerConfiguration()
        {
            // I will replace this with my own custom app config handler
            SoftwareName = ConfigurationManager.AppSettings["SoftwareName"];
            InstantInterval = TimeSpan.Parse(ConfigurationManager.AppSettings["InstantInterval"]);
            DayStart = TimeSpan.Parse(ConfigurationManager.AppSettings["DayStart"]);
            DayEnd = TimeSpan.Parse(ConfigurationManager.AppSettings["DayEnd"]);
            Daily = TimeSpan.Parse(ConfigurationManager.AppSettings["Daily"]);
            Weekly = TimeSpan.Parse(ConfigurationManager.AppSettings["Weekly"]);
            WeeklyDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), ConfigurationManager.AppSettings["WeeklyDay"]);
            RunAtWeekend = Convert.ToBoolean(ConfigurationManager.AppSettings["RunAtWeekend"]);
        }
    }
}
