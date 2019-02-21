using System;

namespace LuKaSo.MarketData.Infrastructure.Common
{
    /// <summary>
    /// Event arguments including progress
    /// </summary>
    public class ProgressReporterEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="progress">Progress</param>
        /// <param name="items">Items</param>
        public ProgressReporterEventArgs(double progress, long items)
        {
            Progress = progress;
            Items = items;
        }

        /// <summary>
        /// Progress
        /// </summary>
        public double Progress { get; protected set; }

        /// <summary>
        /// Items
        /// </summary>
        public long Items { get; protected set; }
    }
}
