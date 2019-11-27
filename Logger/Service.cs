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
        private Thread _thread;

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
            OnStart(new string[] { });

            Console.WriteLine("Starting, press any key to exit...");
            Console.ReadKey();

            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            BeginProcess();
        }


        public bool BeginProcess()
        {
            if (_thread != null && _thread.IsAlive)
            {
                return true;
            }

            _thread = new Thread(ProcessForever) { Name = "Logger Process Thread" };
            _thread.Start();


            return true;
        }

        public void ProcessForever()
        {
            while (!_stopProcessing)
            {
                if (_stopProcessing) break;

                foreach (var schedule in _schedules.Where(x => x.NextRunDate <= DateTime.Now))
                {

                    // Hook into processor class - for now a simple console writeline out.
                    Console.WriteLine("test - " + DateTime.Now + " " + _config.InstantInterval + " Schedule Type:" + schedule.ScheduleType);

                    ProcessSchedule(schedule);
                }

                var next = _schedules.OrderBy(x => x.NextRunDate).First();

                Thread.Sleep(next.NextRunDate.Subtract(DateTime.Now));
            }
        }

        private void ProcessSchedule(IScheduler schedule)
        {
            schedule.SetNextRunDate(_config, DateTime.Now);
        }
        
        private void EndProcess()
        {
            _stopProcessing = true;
            _thread.Join();
        }

        protected override void OnStop()
        {
            EndProcess();
        }
    }
}
