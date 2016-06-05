using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Alfred.IoC;
using LightInject;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(Alfred.Startup))]

namespace Alfred
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            ConfigIoC(config);
            ConfigSwagger(config);
            ConfigWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private void ConfigWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private void ConfigSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", GetAssemblyVersion());
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.DescribeAllEnumsAsStrings();
            }).EnableSwaggerUi();
        }

        private string GetAssemblyVersion()
        {
            return Assembly.GetAssembly(typeof(Startup)).GetName().Version.ToString();
        }

        private void ConfigIoC(HttpConfiguration config)
        {
            var container = new ServiceContainer();
            container.RegisterFrom<WebApiCompositionRoot>();
            container.RegisterApiControllers();
            container.EnableWebApi(config);
        }

        private string GetXmlCommentsPath()
        {
            return $@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\Alfred.XML";
        }
    }
}