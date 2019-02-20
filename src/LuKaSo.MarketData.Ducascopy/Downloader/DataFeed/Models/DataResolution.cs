namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models
{
    /// <summary>
    /// Ducascopy data time resolution
    /// </summary>
    public enum DataResolution
    {
        /// <summary>
        /// Tick data resolution
        /// </summary>
        TickData,

        /// <summary>
        /// 10 second resolution
        /// </summary>
        Second10,

        /// <summary>
        /// Minute resolution
        /// </summary>
        Minute,

        /// <summary>
        /// Hour resolution
        /// </summary>
        Hour,

        /// <summary>
        /// Day resolution
        /// </summary>
        Day
    }
}
