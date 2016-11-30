using System.Web.Http;
using Alfred.IoC;
using LightInject;

namespace Alfred.WebApi.Application
{
    public static class IocConfiguration
    {
        public static HttpConfiguration ConfigIoC(this HttpConfiguration config)
        {
            var container = Bootstrap.CreateContainer(config);                        
            container.RegisterApiControllers();
            container.EnableWebApi(config);
            return config;
        }
    }
}
