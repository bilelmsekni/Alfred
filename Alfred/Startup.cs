using System.Net.Http.Headers;
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

            //-1- IoC
            var container = new ServiceContainer();
            container.RegisterFrom<WebApiCompositionRoot>();
            container.RegisterApiControllers();
            container.EnableWebApi(config);

            //-2- Swagger            
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Alfred");
                c.IncludeXmlComments(GetXmlCommentsPath());
            }).EnableSwaggerUi();

            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            appBuilder.UseWebApi(config);
        }

        private string GetXmlCommentsPath()
        {
            return $@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\Alfred.XML";
        }
    }
}