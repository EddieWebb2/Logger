using Owin;

namespace Logger.Api.Controllers
{
    public interface IController
    {
        void Configure(IAppBuilder app);
    }
}
