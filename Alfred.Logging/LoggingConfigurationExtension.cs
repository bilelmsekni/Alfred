using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Web;
using Owin;
using Serilog;

namespace Alfred.Logging
{
    public static class LoggingConfigurationExtension
    {
        public static IAppBuilder ConfigureLogging(this IAppBuilder app)
        {
            const string defaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} ({RequestId}) {Message}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.RollingFile(Path.Combine(HttpRuntime.AppDomainAppPath, "logs\\alfred-{Date}.txt"), outputTemplate:defaultOutputTemplate))
                .CreateLogger();
            return app.UseSerilogRequestContext();               
        }

        private static IAppBuilder UseSerilogRequestContext(this IAppBuilder app)
        {
            return app.Use(typeof(RequestContextMiddleware));
        }
    }
}
