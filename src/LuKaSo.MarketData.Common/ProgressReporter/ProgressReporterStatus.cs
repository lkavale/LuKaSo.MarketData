namespace LuKaSo.MarketData.Common.ProgressReporter
{
    /// <summary>
    /// Progress reporter status
    /// </summary>
    public enum ProgressReporterStatus
    {
        /// <summary>
        /// Failed
        /// </summary>
        Failed = 3,

        /// <summary>
        /// Finished
        /// </summary>
        Finished = 2,

        /// <summary>
        /// Running
        /// </summary>
        Running = 1,

        /// <summary>
        /// Ready
        /// </summary>
        Ready = 0
    }
}
