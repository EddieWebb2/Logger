using System.Threading.Tasks;
using Logger.Infrastructure;
using Microsoft.Owin;
using Owin;
using Owin.Routing;
using Serilog;

namespace Logger.Api.Controllers
{
    public class ScheduleController: IController
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ScheduleController>();
        private readonly ILoggerConfiguration _config;

        public ScheduleController(ILoggerConfiguration config)
        {
            _config = config;
        }

        public void Configure(IAppBuilder app)
        {
            app.Route("api/schedules").Get(GetSchedules);
        }

        private async Task GetSchedules(IOwinContext context)
        {
            await context.WriteJson("To be wired in");
        }

    }
}
