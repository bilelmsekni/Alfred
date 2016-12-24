using System.Web.Http.Dependencies;

namespace Alfred.WebApi.Application
{
    public static class DependencyResolverConfig
    {
        public static T Resolve<T>(this IDependencyResolver resolver)
        {
            return (T) resolver.GetService(typeof(T));
        }
    }
}
