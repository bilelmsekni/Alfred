using System.Web.Http.Controllers;

namespace Alfred.WebApi.Application.Extensions
{
    public static class ActionContextExtensions
    {
        public static string GetFeatureName(this HttpActionContext actionContext)
        {
            return $"{actionContext?.ActionDescriptor?.ControllerDescriptor?.ControllerName}.{actionContext?.ActionDescriptor?.ActionName}";
        }
    }
}
