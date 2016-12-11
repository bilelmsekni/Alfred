using System;
using System.Collections.Generic;

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
    }
}
