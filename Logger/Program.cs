using System;
using System.ServiceProcess;

namespace Logger
{
    static class Program
    {
        static void Main(string[] args)
        {
            var service = new Service();

            if (Environment.UserInteractive)
                service.RunConsole();
            else
                 ServiceBase.Run(new ServiceBase[] {service});

        }
    }
}
