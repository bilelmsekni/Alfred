using System.Web.Http.ExceptionHandling;
using Alfred.Logging;
using Alfred.Logging.Events;
using Alfred.WebApi.Application.Extensions;

namespace Alfred.WebApi.Application.Services
{
    public class AdvancedExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            LogFactory.LogTechnicalEvent(EventFactory.CreateTechnicalEvent(context.ExceptionContext.ActionContext?.GetFeatureName(),
                context.ExceptionContext.ActionContext?.ActionArguments, context.Request.RequestUri,
                context.Exception, context.ExceptionContext.Response.GetStatusCode()));
        }
    }
}
