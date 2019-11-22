using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using Logger.Infrastructure;
using Logger.Schedules;

namespace Logger
{
    partial class Service : ServiceBase
    {
        private readonly ILoggerConfiguration _config;
        private List<IScheduler> _schedules;
        private readonly IScheduleHelper _scheduleHelper;

        private bool _stopProcessing;

        public Service(ILoggerConfiguration config, IScheduleHelper scheduleHelper)
        {
            _config = config;
            _scheduleHelper = scheduleHelper;

            InitializeComponent();

            ServiceConfiguration();
        }

        public void ServiceConfiguration()
        {
            _schedules = new List<IScheduler>
            {
                new InstantSchedule(_scheduleHelper),
                new DailySchedule(_scheduleHelper),
                new WeeklySchedule(_scheduleHelper)
            };

            foreach (var sch in _schedules)
            {
                sch.SetNextRunDate(_config, DateTime.Now);
            }
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
            while (!_stopProcessing)
            {
                if (_stopProcessing) break;

                foreach (var schedule in _schedules.Where(x => x.NextRunDate <= DateTime.Now))
                {
                    Console.WriteLine("test");

                    schedule.SetNextRunDate(_config, DateTime.Now);
                }

                var next = _schedules.OrderBy(x => x.NextRunDate).First();

                Thread.Sleep(next.NextRunDate.Subtract(DateTime.Now));
            }
        }

        protected override void OnStop()
        {
            _stopProcessing = true;
        }
    }
}
