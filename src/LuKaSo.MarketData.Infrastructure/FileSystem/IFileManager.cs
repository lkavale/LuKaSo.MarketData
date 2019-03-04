using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.FileSystem
{
    public interface IFileManager<T>
        where T : ISymbol
    {
        /// <summary>
        /// Get start date of downloaded data
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        DateTime GetStartDateTime(T symbol);

        /// <summary>
        /// Get last date of downloaded data
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        DateTime GetEndDateTime(T symbol);

        /// <summary>
        /// Get date and times of downloaded files
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        IEnumerable<DateTime> GetDateTimes(T symbol);
    }
}
