using System;

namespace LuKaSo.MarketData.Types.Downloader
{
    /// <summary>
    /// Download file
    /// </summary>
    public class DownloaderFile
    {
        #region Properties

        /// <summary>
        /// Time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// File
        /// </summary>
        public string File { get; set; }

        #endregion

        #region Navigation properties

        /// <summary>
        /// Downloader item
        /// </summary>
        public virtual DownloaderItem DownloaderItem { get; set; }

        #endregion
    }
}
