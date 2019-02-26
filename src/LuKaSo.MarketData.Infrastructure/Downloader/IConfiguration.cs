using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IConfiguration
    {
        /// <summary>
        /// Default data path
        /// </summary>
        string DataPath { get; set; }

        /// <summary>
        /// Base source
        /// </summary>
        string BaseSource { get; }
    }
}
