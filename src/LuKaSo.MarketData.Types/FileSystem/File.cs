using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using System;

namespace LuKaSo.MarketData.Types.FileSystem
{
    /// <summary>
    /// Download file
    /// </summary>
    public class File : IFile
    {
        #region Properties

        /// <summary>
        /// Time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// URL of source file
        /// </summary>
        public Uri SourceFile { get; set; }

        /// <summary>
        /// Destination file
        /// </summary>
        public string DestinationFile { get; set; }

        #endregion

        #region Navigation properties

        /// <summary>
        /// Downloader item
        /// </summary>
        public virtual IDownloaderItem DownloaderItem { get; set; }

        #endregion
    }
}
