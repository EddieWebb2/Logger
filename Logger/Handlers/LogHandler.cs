using System;
using System.IO;
using System.ServiceProcess;
using Logger.Infrastructure;
using Serilog;
using Serilog.Events;

namespace Logger.Handlers
{
    public class LogHandler
    {
        public static void InitializeLogging(ILoggerConfiguration config)
        {
            string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

            Serilog.LoggerConfiguration loggerConfiguration = new Serilog.LoggerConfiguration().MinimumLevel.Debug().Enrich
                .WithProperty("softwareName", (object) config.SoftwareName, false).Enrich.FromLogContext().WriteTo
                .ColoredConsole(LogEventLevel.Verbose,
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}", (IFormatProvider) null)
                .WriteTo.RollingFile(Path.Combine(path1, config.SoftwareName + ".log"), LogEventLevel.Verbose,
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                    (IFormatProvider) null, new long?(1073741824L), new int?(31));

            Log.Logger = loggerConfiguration.CreateLogger();

            Log.ForContext<ServiceBase>().Information("Configuration: {@config}", (object) config);

            AppDomain.CurrentDomain.UnhandledException += (UnhandledExceptionEventHandler) ((sender, e) =>
            {
                Exception exceptionObject = e.ExceptionObject as Exception;
                Log.ForContext<ServiceBase>().Error(exceptionObject, exceptionObject.Message);
            });
        }
    }
}
