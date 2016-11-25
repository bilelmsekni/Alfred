using System.Linq;
using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Owin.Cors;
using Owin;

namespace Alfred.WebApi.Application
{
    public static class CorsConfiguration
    {
        public static IAppBuilder UseCors(this IAppBuilder app, IConfiguration appSettings)
        {
            var corsPolicy = new CorsPolicy
            {
                SupportsCredentials = true
            };

            var headers = appSettings["Alfred:AppSettings:Cors:Headers"];
            var methods = appSettings["Alfred:AppSettings:Cors:Methods"];
            var origins = appSettings["Alfred:AppSettings:Cors:Origins"];

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(
                        corsPolicy.AddHeaders(headers)
                        .AddMethods(methods)
                        .AddOrigins(origins))
                }
            };

            return app.UseCors(corsOptions);
        }

        private static CorsPolicy AddHeaders(this CorsPolicy corsPolicy, string headers)
        {
            if (string.IsNullOrEmpty(headers))
            {
                corsPolicy.AllowAnyHeader = true;
            }
            else
            {
                headers.Split(';').ToList().ForEach(h => corsPolicy.Headers.Add(h));
            }
            return corsPolicy;
        }

        private static CorsPolicy AddOrigins(this CorsPolicy corsPolicy, string origins)
        {
            if (string.IsNullOrEmpty(origins))
            {
                corsPolicy.AllowAnyOrigin = true;
            }
            else
            {
                origins.Split(';').ToList().ForEach(h => corsPolicy.Origins.Add(h));
            }
            return corsPolicy;
        }

        private static CorsPolicy AddMethods(this CorsPolicy corsPolicy, string methods)
        {
            if (string.IsNullOrEmpty(methods))
            {
                corsPolicy.AllowAnyOrigin = true;
            }
            else
            {
                methods.Split(';').ToList().ForEach(h => corsPolicy.Origins.Add(h));
            }
            return corsPolicy;
        }
    }
}
