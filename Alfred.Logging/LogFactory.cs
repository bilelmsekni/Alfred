using Alfred.Logging.Events;
using Serilog;

namespace Alfred.Logging
{
    public class LogFactory
    {   
        public static void LogTechnicalEvent(TechnicalEvent technicalEvent)
        {
            Log.Information("FeatureName: {FeatureName}", technicalEvent.FeatureName);
            Log.Information("RequestUrl {RequestUrl}", @technicalEvent.RequestUrl);
            Log.Information("Exception: {ElapsedTime}", technicalEvent.EventException);
            Log.Information("ActionArguments {ActionArguments}", @technicalEvent.ActionArguments);
            Log.Information("StatusCode: {StatusCode}", technicalEvent.StatusCode);

        }

        public static void LogFunctionalEvent(FunctionalEvent functionalEvent)
        {
            Log.Information("FeatureName: {FeatureName}", functionalEvent.FeatureName);
            Log.Information("Performance: {ElapsedTime}", functionalEvent.ElapsedTime);
            Log.Information("StatusCode: {StatusCode}", functionalEvent.StatusCode);
            Log.Information("ActionArguments {ActionArguments}", @functionalEvent.ActionArguments);           
        }
    }
}
