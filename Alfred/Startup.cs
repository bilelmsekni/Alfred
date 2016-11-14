using System.Web.Http;
using Alfred.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Alfred.Startup))]

namespace Alfred
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration()
                .ConfigIoC()
                .ConfigSwagger()
                .ConfigWebApi();

            appBuilder
                .UseCors(config.DependencyResolver.Resolve<IConfiguration>())
                .UseWebApi(config);
        }
    }
}