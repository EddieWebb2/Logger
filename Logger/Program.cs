using System;
using System.ServiceProcess;
using Logger.Handlers;
using Logger.Infrastructure;
using Serilog;
using StructureMap;
using LoggerConfiguration = Logger.Infrastructure.LoggerConfiguration;

namespace Logger
{
    static class Program
    {
        static void Main(string[] args)
        {
            LoggerConfiguration _config = new LoggerConfiguration();
            LogHandler.InitializeLogging(_config);

            var container = new Container(new LoggerRegistry(_config));
            Log.Debug("Logger registry set up");

            var service = container.GetInstance<Service>();

            if (Environment.UserInteractive)
                service.RunConsole();
            else
                 ServiceBase.Run(new ServiceBase[] {service});
        }
    }
}
