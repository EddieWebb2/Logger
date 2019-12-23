using Logger.Api.Controllers;
using Logger.Models;
using Logger.Persist.InMemory;
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

                a.AddAllTypesOf<IController>();
            });

            For<ILoggerConfiguration>().Use(config);
            For<IScheduleHelper>().Use(new ScheduleHelper(config));

            For<Schedule>().Use<InMemoryRepository<Schedule>>();
        }
    }
}
