using System.Net.Http.Headers;
using System.Web.Http;
using Alfred.WebApi.Application.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alfred.WebApi.Application
{
    public static class WebApiConfiguration
    {
        public static HttpConfiguration ConfigWebApi(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new LoggingActionFilter());
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return config;
        }
    }
}
