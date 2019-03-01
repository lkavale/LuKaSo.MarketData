using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using System;

namespace LuKaSo.MarketData.Ducascopy.FileSystem
{
    public class DucascopyFileNameGenerator : IFileNameGenerator<DucascopySymbol>
    {
        /// <summary>
        /// Create file path
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GenerateFileName(DucascopySymbol symbol, DateTime dateTime)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}h_ticks.bi5",
                                symbol.DirectoryName,
                                dateTime.Year.ToString("D2"),
                                (dateTime.Month - 1).ToString("D2"),
                                dateTime.Day.ToString("D2"),
                                dateTime.Hour.ToString("D2"));
        }
    }
}
