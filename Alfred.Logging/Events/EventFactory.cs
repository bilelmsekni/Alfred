using System;
using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Alfred.Logging.Events
{
    public class EventFactory
    {
        public static FunctionalEvent CreateFunctionalEvent(string featureName,
            TimeSpan elapsedTime,
            int statusCode,
            Dictionary<string, object> actionArguments)
        {
            return new FunctionalEvent
            {
                FeatureName = featureName,
                ElapsedTime = elapsedTime,
                StatusCode = statusCode,
                ActionArguments = actionArguments
            };
        }

        public static TechnicalEvent CreateTechnicalEvent(string featureName, Dictionary<string, object> actionArguments, Uri url, 
            Exception exception, int statusCode)
        {
            return new TechnicalEvent
            {
                FeatureName = featureName,
                ActionArguments = actionArguments,
                EventException = exception,
                RequestUrl = url,
                StatusCode = statusCode
            };
        }
    }
}
