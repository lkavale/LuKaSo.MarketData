using LuKaSo.MarketData.Infrastructure.Common;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IDownloaderItem
    {
        /// <summary>
        /// Date from - range of data exists
        /// </summary>
        DateTime DateFrom { get; set; }

        /// <summary>
        /// Date to - range of data exists
        /// </summary>
        DateTime DateTo { get; set; }

        /// <summary>
        /// Date from - range of data download
        /// </summary>
        DateTime? DateFromDesired { get; set; }

        /// <summary>
        /// Date to - range of data download
        /// </summary>
        DateTime? DateToDesired { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        DownloaderItemStatus Status { get; set; }

        /// <summary>
        /// Progress indicator
        /// </summary>
        IProgressReporter Indicator { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        ISymbol Symbol { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        IList<IFile> Files { get; set; }
    }
}
