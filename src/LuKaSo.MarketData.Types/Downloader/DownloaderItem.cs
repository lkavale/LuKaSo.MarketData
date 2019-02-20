using LuKaSo.MarketData.Common.Infrastructure;
using LuKaSo.MarketData.Types.Instruments;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Types.Downloader
{
    /// <summary>
    /// Downloader item
    /// </summary>
    public class DownloaderItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DownloaderItem()
        {
            Active = true;
            Files = new List<DownloaderFile>();
            Status = DownloaderItemStatus.Ready;
        }

        #region Properties
        /// <summary>
        /// Date from - range of data exists
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Date to - range of data exists
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Date from - range of data download
        /// </summary>
        public DateTime? DateFromDesired { get; set; }

        /// <summary>
        /// Date to - range of data download
        /// </summary>
        public DateTime? DateToDesired { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public DownloaderItemStatus Status { get; set; }

        /// <summary>
        /// Progress indicator
        /// </summary>
        public IProgressReporter Indicator { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }

        #endregion

        #region Navigation properties

        /// <summary>
        /// Symbol
        /// </summary>
        public virtual Symbol Symbol { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        public List<DownloaderFile> Files { get; set; }

        #endregion
    }
}
