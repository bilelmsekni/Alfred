using System.Web.Http;
using Alfred.Configuration;
using Alfred.Logging;
using Alfred.WebApi;
using Alfred.WebApi.Application;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Alfred.WebApi
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
                .ConfigureLogging(AppSettingsProvider.Build<LoggingConfiguration>())
                .UseCors(AppSettingsProvider.Build<CorsConfiguration>())
                .UseWebApi(config);
        }
    }
}