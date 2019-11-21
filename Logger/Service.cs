using System;
using System.ServiceProcess;

namespace Logger
{
    partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        public void RunConsole()
        {
            OnStart(new string[] {});

            Console.WriteLine("Starting, press any key to exit...");
            Console.ReadKey();

            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
