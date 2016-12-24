using System.Linq;
using System.Threading.Tasks;
using System.Web.Cors;
using Alfred.Configuration;
using Microsoft.Owin.Cors;
using Owin;

namespace Alfred.WebApi.Application
{
    public static class CorsConfig
    {
        public static IAppBuilder UseCors(this IAppBuilder app, CorsConfiguration appSettings)
        {
            var corsPolicy = new CorsPolicy
            {
                SupportsCredentials = true
            };

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(
                        corsPolicy.AddHeaders(appSettings.Headers)
                        .AddMethods(appSettings.Methods)
                        .AddOrigins(appSettings.Origins))
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
