using System;
using System.IO;

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
        Uri BaseSource { get; }
    }
}
