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
            LoggerConfiguration _config = new LoggerConfiguration();

            var service = new Service();

            if (Environment.UserInteractive || _config.Mode == ReleaseModes.Dev)
                service.RunConsole();
            else
                 ServiceBase.Run(new ServiceBase[] {service});

        }
    }
}
