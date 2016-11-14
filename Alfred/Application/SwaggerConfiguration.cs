using System;
using System.Reflection;
using System.Web.Http;
using Swashbuckle.Application;

namespace Alfred.Application
{
    public static class SwaggerConfiguration
    {
        public static HttpConfiguration ConfigSwagger(this HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", GetAssemblyVersion());
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.DescribeAllEnumsAsStrings();
            }).EnableSwaggerUi();
            return config;
        }

        private static string GetAssemblyVersion()
        {
            return Assembly.GetAssembly(typeof(Startup)).GetName().Version.ToString();
        }

        private static string GetXmlCommentsPath()
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Alfred.XML";
        }
    }
}
