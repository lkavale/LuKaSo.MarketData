using LuKaSo.MarketData.Infrastructure.Downloader;
using System;

namespace LuKaSo.MarketData.Infrastructure.FileSystem
{
    public interface IFile
    {
        /// <summary>
        /// Time
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>
        /// URL of source file
        /// </summary>
        string SourceFile { get; set; }

        /// <summary>
        /// Destination file
        /// </summary>
        string DestinationFile { get; set; }

        /// <summary>
        /// Downloader item
        /// </summary>
        IDownloaderItem DownloaderItem { get; set; }
    }
}
