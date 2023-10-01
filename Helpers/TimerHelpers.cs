using SudokuGame.Models;
using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace SudokuGame.Helpers
{
    /// <summary>
    /// timer helper utils
    /// </summary>
    static class TimerHelpers
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
            _stopwatch = new Stopwatch();
            _timer = new Timer(1000);
            _timer.Elapsed += updatedTimer;

            _timer.Start();
            _stopwatch.Restart();
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
                _ = ex;
            }
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
