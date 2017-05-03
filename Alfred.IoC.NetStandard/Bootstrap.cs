using System;
using Microsoft.Extensions.DependencyInjection;

namespace Alfred.IoC.NetStandard
{
    public static class Bootstrap
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IArtifactService, ArtifactService>();
            return services;
        }
    }
}
