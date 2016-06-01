using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Alfred.Startup))]

namespace Alfred
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello, world.");
            });
        }
    }
}