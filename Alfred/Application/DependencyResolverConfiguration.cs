using System.Web.Http.Dependencies;

namespace Alfred.Application
{
    public static class DependencyResolverConfiguration
    {
        public static T Resolve<T>(this IDependencyResolver resolver)
        {
            return (T) resolver.GetService(typeof(T));
        }
    }
}
