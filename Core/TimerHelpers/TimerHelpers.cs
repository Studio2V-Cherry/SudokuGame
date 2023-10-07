using Core.CrashlyticsHelpers;
using Core.Models;
using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace Core.TimerHelpers
{
    /// <summary>
    /// timer helper utils
    /// </summary>
    static public class TimerHelpers
    {
        /// <summary>
        /// The timer
        /// </summary>
        private static Stopwatch _stopwatch;

        /// <summary>
        /// The timer
        /// </summary>
        private static Timer _timer;
        //<summary>
        /// <summary>
        /// Occurs when [update timer].
        /// </summary>
        public static event EventHandler<TimerHelperEventArgs> UpdateTimer;
        /// <summary>
        /// Starts the timer.
        /// </summary>
        public static void startTimer()
        {
            try
            {
                _stopwatch = new Stopwatch();
                _timer = new Timer(1000);
                _timer.Elapsed += updatedTimer;

                _timer.Start();
                _stopwatch.Restart();
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        public static void resumeTimer()
        {
            try
            {
                _timer = new Timer(1000);
                _timer.Elapsed += updatedTimer;

                _timer.Start();
                _stopwatch.Start();
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Updateds the timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private static void updatedTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                TimerHelperEventArgs timerHelperEventArgs = new TimerHelperEventArgs()
                {
                    Timer = $"{_stopwatch.Elapsed.Minutes}:{_stopwatch.Elapsed.Seconds}",
                };

                UpdateTimer.Invoke(null, timerHelperEventArgs);
            }
            catch (Exception ex)
            {
                CrashLogger.LogException(ex);
            }
        }

        /// <summary>
        /// Gets the elapsed.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan getElapsed()
        {
            return _stopwatch.Elapsed;
        }

        /// <summary>
        /// Sets the timer.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public static void setTimer(long elapsed)
        {
            try
            {
                _stopwatch = _stopwatch ?? new Stopwatch();
                _stopwatch.Elapsed.Add(new TimeSpan(elapsed));
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        public static void resetTimer()
        {
            try
            {
                if (_timer != null)
                {
                    _stopwatch = new Stopwatch();
                    _timer.Stop();
                    _timer.Elapsed -= updatedTimer;
                }
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Gets the stopwatch instnace.
        /// </summary>
        /// <returns></returns>
        public static Stopwatch getStopwatchInstnace()
        {
            return _stopwatch;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public static void stopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Elapsed -= updatedTimer;
            }
        }
    }
}
