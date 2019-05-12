using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.FileSystem
{
    public interface IFileManager<T>
        where T : ISymbol
    {
        /// <summary>
        /// Get missing files for symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<IFile> GetMissingFiles(T symbol, DateTime start, DateTime end);

        /// <summary>
        /// Get date and times of missing files
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<DateTime> GetMissingDates(T symbol, DateTime start, DateTime end);

        /// <summary>
        /// Get exisiting files for symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<IFile> GetExistingFiles(T symbol, DateTime start, DateTime end);

        /// <summary>
        /// Get date and times of existing files
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<DateTime> GetExistingDates(T symbol, DateTime start, DateTime end);
    }
}
