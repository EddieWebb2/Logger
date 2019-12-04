using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Logger.Api.Controllers;
using Logger.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using Owin.Routing;
using Serilog;

namespace Logger.Api
{
    public class ApiHost
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ApiHost>();
        private readonly ILoggerConfiguration _config;
        private readonly List<IController> _controllers;

        public ApiHost(ILoggerConfiguration config, IEnumerable<IController> controller)
        {
            _config = config;
            _controllers = controller.ToList();
        }

        public void Configure(IAppBuilder app)
        {
            app.Route("api/").Get(GetIndex);
        }

        public IDisposable Start()
        {
            return WebApp.Start(_config.LoggerServiceEndpoint, app =>
            {
                app.Use<SerilogMiddleware>();
                app.UseCors(CorsOptions.AllowAll);
                foreach (var controller in _controllers)
                {
                    controller.Configure(app);
                    Configure(app);
                }
            });

        }

        private async Task GetIndex(IOwinContext context)
        {
            var links = new Dictionary<string, string>
            {
                { "Description","Branding Service for Gattaca Applications" },
                { "version",Assembly.GetExecutingAssembly().GetName().Version.ToString() },
                { "self","/" }
            };

            var index = new {Links = links};
            await context.WriteJson(index);
        }
    }
}
