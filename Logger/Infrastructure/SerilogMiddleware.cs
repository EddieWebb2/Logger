using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;
using Serilog.Context;

namespace Logger.Infrastructure
{
    public class SerilogMiddleware : OwinMiddleware
    {
        public SerilogMiddleware(OwinMiddleware next): base(next){ }

        public override async Task Invoke(IOwinContext context)
        {
            var log = Log.ForContext<SerilogMiddleware>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                await Next.Invoke(context);
            }
            finally
            {
                stopWatch.Stop();

                var request = context.Request;
                var response = context.Response;

                using (LogContext.PushProperty("sourceAddress", request.RemoteIpAddress))
                {
                    log.Information(
                        "Api {httpMethod} {httpCode} to {url} took {elapsed}ms",
                        request.Method,
                        response.StatusCode + " " +
                        Enum.Parse(typeof(HttpStatusCode), Convert.ToString(response.StatusCode)),
                        request.Uri,
                        stopWatch.ElapsedMilliseconds);
                }
            }
        }
    }
}