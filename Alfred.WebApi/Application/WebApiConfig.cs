using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Alfred.WebApi.Application.Filters;
using Alfred.WebApi.Application.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alfred.WebApi.Application
{
    public static class WebApiConfig
    {
        public static HttpConfiguration ConfigWebApi(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new LoggingActionFilter());
            config.Services.Replace(typeof(IExceptionLogger), new AdvancedExceptionLogger());
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return config;
        }
    }
}
