using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Context;

namespace Alfred.Logging
{
    public class RequestContextMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;
        public const string DefaultRequestIdPropertyName = "RequestId";

        public RequestContextMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            if (next == null)
            {
                throw new ArgumentException("next");
            }

            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            using (LogContext.PushProperty(DefaultRequestIdPropertyName, Guid.NewGuid()))
            {
                await _next(environment);
            }
        }
    }
}
