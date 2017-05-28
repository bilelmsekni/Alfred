using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Alfred.WebApi.NetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Alfred Api", Version = "v1" });
                c.IncludeXmlComments(GetXmlCommentsPath());
            });
            return services;
        }

        private static string GetXmlCommentsPath()
        {
            return $@"{ PlatformServices.Default.Application.ApplicationBasePath}\Alfred.WebApi.NetCore.xml";
        }
    }
}
