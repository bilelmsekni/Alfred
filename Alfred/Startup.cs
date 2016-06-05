using System.Net.Http.Headers;
using System.Web.Http;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.FakeImplementation.Repositories;
using Alfred.Dal.Interfaces;
using LightInject;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Alfred.Startup))]

namespace Alfred
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            var container = new ServiceContainer();
            container.RegisterFrom<WebApiCompositionRoot>();
            container.EnableWebApi(config);          
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            appBuilder.UseWebApi(config);
        }
    }

    public class WebApiCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IMemberRepository, MemberRepository>();
            serviceRegistry.Register<IMemberDao, MemberDao>();
            serviceRegistry.RegisterApiControllers(typeof(Startup).Assembly);
        }
    }
}