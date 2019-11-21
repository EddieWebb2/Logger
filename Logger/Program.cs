using System;
using System.ServiceProcess;
using Logger.Infrastructure;
using Logger.Types;

namespace Logger
{
    static class Program
    {
        static void Main(string[] args)
        {
            LoggerConfiguration config = new LoggerConfiguration();

            var service = new Service();

            if (Environment.UserInteractive || config.Mode == ReleaseModes.Dev)
                service.RunConsole();
            else
                 ServiceBase.Run(new ServiceBase[] {service});

        }
    }
}
