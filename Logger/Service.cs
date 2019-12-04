using Logger.Infrastructure;
using Logger.Schedules;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using Logger.Api;

namespace Logger
{
    partial class Service : ServiceBase
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<Service>();

        private readonly ILoggerConfiguration _config;
        private readonly IScheduleHelper _scheduleHelper;
        private readonly ApiHost _api;
        private bool _stopProcessing;
        private IDisposable _host;
        private List<IScheduler> _schedules;
        private Thread _thread;

        public Service(ILoggerConfiguration config, IScheduleHelper scheduleHelper, ApiHost api)
        {
            _config = config;
            _scheduleHelper = scheduleHelper;
            _api = api;

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
                Log.ForContext<Service>().Information($"Next run {sch.NextRunDate}");
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
            _host = _api.Start();
            BeginProcess();
            Log.Information("Started Logger");
        }


        public bool BeginProcess()
        {
            if (_thread != null && _thread.IsAlive)
            {
                Log.Debug("Logger Process Thread already exists");
                return true;
            }

            _thread = new Thread(ProcessForever) { Name = "Logger Process Thread" };
            _thread.Start();

            Log.Debug("Logger Process Thread has been started");

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
            Log.Debug($"Set schedule next run date: {schedule.NextRunDate}");
        }
        
        private void EndProcess()
        {
            _stopProcessing = true;
            _thread.Join();
        }

        protected override void OnStop()
        {
            EndProcess();
            Log.ForContext<Service>().Information("Service Was Stopped");
        }
    }
}
