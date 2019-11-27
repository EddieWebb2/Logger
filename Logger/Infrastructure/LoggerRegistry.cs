using Logger.Schedules;
using StructureMap;

namespace Logger.Infrastructure
{
    public class LoggerRegistry: Registry
    {
        public LoggerRegistry(ILoggerConfiguration config)
        {
            Scan(a =>
            {
                a.TheCallingAssembly();
                a.WithDefaultConventions();
                a.LookForRegistries();
            });

            For<ILoggerConfiguration>().Use(config);
            For<IScheduleHelper>().Use(new ScheduleHelper(config));
        }
    }
}
