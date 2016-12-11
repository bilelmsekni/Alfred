using Alfred.Logging.Events;
using Serilog;

namespace Alfred.Logging
{
    public class LogFactory
    {   
        public static void LogTechnicalEvent(TechnicalEvent technicalEvent)
        {
            Log.Information("Performance: {ElapsedTime}", technicalEvent.EventException);
            Log.Information("FeatureName: {FeatureName}", technicalEvent.FeatureName);
        }

        public static void LogFunctionalEvent(FunctionalEvent functionalEvent)
        {
            Log.Information("Performance: {ElapsedTime}", functionalEvent.ElapsedTime);
            Log.Information("FeatureName: {FeatureName}", functionalEvent.FeatureName);
            Log.Information("StatusCode: {StatusCode}", functionalEvent.StatusCode);
            Log.Information("ActionArguments {ActionArguments}", @functionalEvent.ActionArguments);
        }
    }
}
