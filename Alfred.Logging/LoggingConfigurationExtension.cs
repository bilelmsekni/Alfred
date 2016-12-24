using System.IO;
using System.Web;
using Alfred.Configuration;
using Owin;
using Serilog;

namespace Alfred.Logging
{
    public static class LoggingConfigurationExtension
    {
        public static IAppBuilder ConfigureLogging(this IAppBuilder app, LoggingConfiguration loggingConfig)
        {
            //const string defaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} ({RequestId}) {Message}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.RollingFile(Path.Combine(HttpRuntime.AppDomainAppPath, "logs\\alfred-{Date}.txt"), outputTemplate: loggingConfig.DefaultOutput))
                .CreateLogger();
            return app.UseSerilogRequestContext();               
        }

        private static IAppBuilder UseSerilogRequestContext(this IAppBuilder app)
        {
            return app.Use(typeof(RequestContextMiddleware));
        }
    }
}
