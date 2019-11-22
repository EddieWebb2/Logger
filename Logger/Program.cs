using System;
using System.ServiceProcess;
using Logger.Infrastructure;
using StructureMap;

namespace Logger
{
    static class Program
    {
        static void Main(string[] args)
        {
            LoggerConfiguration _config = new LoggerConfiguration();

            var container = new Container(new LoggerRegistry(_config));
            var service = container.GetInstance<Service>();

            if (Environment.UserInteractive)
                service.RunConsole();
            else
                 ServiceBase.Run(new ServiceBase[] {service});
        }
    }
}
