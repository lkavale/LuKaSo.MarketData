using System;

namespace LuKaSo.MarketData.Infrastructure.Common
{
    /// <summary>
    /// Progress change event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    public delegate void ProgressChangeEventHandler(object sender, ProgressReporterEventArgs e);

    /// <summary>
    /// Progress reporter interface
    /// </summary>
    public interface IProgressReporter : IProgress<long>
    {
        /// <summary>
        /// Progress changed event
        /// </summary>
        event ProgressChangeEventHandler ProgressChanged;

        /// <summary>
        /// Start progress
        /// </summary>
        /// <param name="totalItems">Total items</param>
        void Start(long totalItems);

        /// <summary>
        /// Finish
        /// </summary>
        void Finish();

        /// <summary>
        /// Failed
        /// </summary>
        void Failed();

        #region Properties

        /// <summary>
        /// Progress in pct
        /// </summary>
        double Progress { get; }

        /// <summary>
        /// Progress
        /// </summary>
        long Items { get; }

        /// <summary>
        /// Maximum
        /// </summary>
        long TotalItems { get; }

        /// <summary>
        /// Status
        /// </summary>
        ProgressReporterStatus Status { get; set; }

        /// <summary>
        /// Start time
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// End time
        /// </summary>
        DateTime? EndTime { get; }

        /// <summary>
        /// Duration
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Elapsed
        /// </summary>
        TimeSpan Elapsed { get; }

        #endregion

    }
}
