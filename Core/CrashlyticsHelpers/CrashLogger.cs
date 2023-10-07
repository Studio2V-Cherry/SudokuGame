using Core.Constants;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Runtime.CompilerServices;

namespace Core.CrashlyticsHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CrashLogger
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public async static void Initialize()
        {
            AppCenter.Configure("f6268215-ab9d-41c3-b2ce-8bfc675d41ca");
            if (AppCenter.Configured)
            {
                AppCenter.LogLevel = LogLevel.Verbose;               
                AppCenter.Start(typeof(Analytics));
                AppCenter.Start(typeof(Crashes));
            }
            if (!await AppCenter.IsEnabledAsync())
                _ = AppCenter.SetEnabledAsync(true);
        }

        /// <summary>
        /// Logs the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public static void LogException(Exception e)
        {
            Crashes.TrackError(e);
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="loggerType">Type of the logger.</param>
        /// <param name="callername">The callername.</param>
        public static void TrackEvent(loggerEnum loggerType, [CallerMemberName] string callername = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>
            {
                { loggerType.ToString(), "event" }
            };
            Analytics.TrackEvent(callername, args);
        }
    }
}
