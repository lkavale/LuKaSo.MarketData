using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    /// <summary>
    /// Downloader item status 
    /// </summary>
    public enum DownloaderItemStatus
    {
        /// <summary>
        /// Ready
        /// </summary>
        Ready,

        /// <summary>
        /// Downloading, waiting for target operation finished
        /// </summary>
        Downloading,

        /// <summary>
        /// Pending, waiting for target operation starts
        /// </summary>
        Pending,

        /// <summary>
        /// Completed
        /// </summary>
        Completed,

        /// <summary>
        /// Incomplete
        /// </summary>
        Incomplete,

        /// <summary>
        /// Failed
        /// </summary>
        Failed
    }
}
