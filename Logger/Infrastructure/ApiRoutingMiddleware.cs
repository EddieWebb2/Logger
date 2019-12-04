using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Logger.Infrastructure
{
    public class ApiRoutingMiddleware : OwinMiddleware
    {
        public ApiRoutingMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ReasonPhrase = e.Message;
            }
        }
    }
}