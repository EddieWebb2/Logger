using Logger.Types;

namespace Logger.Infrastructure
{
    public interface ILoggerConfiguration
    {
        ReleaseModes Mode { get; set; }
    }
}
