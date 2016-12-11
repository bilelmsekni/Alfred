using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Alfred.Logging;
using Alfred.Logging.Events;
using Alfred.WebApi.Application.Extensions;

namespace Alfred.WebApi.Application.Filters
{
    public class LoggingActionFilter : ActionFilterAttribute
    {
        private const string WatchKey = nameof(LoggingActionFilter);

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var watch = new Stopwatch();
            watch.Start();
            actionContext.ActionArguments.Add(WatchKey, watch);
            return Task.CompletedTask;
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var watch = (Stopwatch)actionExecutedContext.ActionContext.ActionArguments[WatchKey];
            watch.Stop();
            LogFactory.LogFunctionalEvent(EventFactory.CreateFunctionalEvent(actionExecutedContext.ActionContext.GetFeatureName(),
                watch.Elapsed,
                actionExecutedContext.Response.GetStatusCode(),
                actionExecutedContext.ActionContext.ActionArguments));
            return Task.CompletedTask;
        }
    }
}
