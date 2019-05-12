using LuKaSo.MarketData.Infrastructure.Common;
using System;
using System.Text;
using System.Timers;

namespace LuKaSo.MarketData.Common.ProgressReporter
{
    /// <summary>
    /// An ASCII progress bar
    /// </summary>
    public class ConsoleProgressReporter : CommonProgressReporter, IDisposable
    {
        /// <summary>
        /// Status
        /// </summary>
        private ProgressReporterStatus _status;

        public override ProgressReporterStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                base.HandleStatusChange(_status, value);
                _status = value;

                UpdateProgress();
            }
        }

        /// <summary>
        /// Lock
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Is disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Progressbar length
        /// </summary>
        private readonly int _progressBarLength;

        /// <summary>
        /// Timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Animation sequence
        /// </summary>
        private const string _animation = @"|/-\";

        /// <summary>
        /// Animation index
        /// </summary>
        private int animationIndex = 0;

        /// <summary>
        /// Current displayed text
        /// </summary>
        private string currentText = string.Empty;

       
        public ConsoleProgressReporter(int progressBarLength, string taskName)
        {
            _progressBarLength = progressBarLength;
            Console.Write(taskName + ": ");
        }

        ~ConsoleProgressReporter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Starts progress
        /// </summary>
        /// <param name="totalItems"></param>
        public override void Start(long totalItems)
        {
            base.Start(totalItems);

            _timer = new Timer(100);
            _timer.Elapsed += Timer_Elapsed;

            // A progress bar is only for temporary display in a console window.
            // If the console output is redirected to a file, draw nothing.
            // Otherwise, we'll end up with a lot of garbage in the target file.
            if (!Console.IsOutputRedirected)
            {
                _timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            if (_disposed)
            {
                return;
            }

            lock (_lock)
            {
                if (_status != ProgressReporterStatus.Running)
                {
                    _timer.Stop();
                    UpdateText("Completed");

                    return;
                }

                int passedChars = (int)(_progressBarLength * Progress / 100);

                string text = string.Format("[{0}{2}{1}] {3}%",
                    new string('#', passedChars),
                    new string('-', _progressBarLength - passedChars),
                    _animation[animationIndex++ % _animation.Length],
                    (int)Progress);

                UpdateText(text);
            }
        }

        private void UpdateText(string text)
        {
            // Get length of common portion
            int commonPrefixLength = 0;
            int commonLength = Math.Min(currentText.Length, text.Length);
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == currentText[commonPrefixLength])
            {
                commonPrefixLength++;
            }

            // Backtrack to the first differing character
            StringBuilder outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', currentText.Length - commonPrefixLength);

            // Output new suffix
            outputBuilder.Append(text.Substring(commonPrefixLength));

            // If the new text is shorter than the old one: delete overlapping characters
            int overlapCount = currentText.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }

            Console.Write(outputBuilder);
            currentText = text;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            lock (_lock)
            {
                if (_disposed)
                {
                    return;
                }

                _timer.Dispose();
                _disposed = true;
            }
        }
    }
}