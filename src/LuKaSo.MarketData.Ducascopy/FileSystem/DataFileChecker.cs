using LuKaSo.MarketData.Common.Extensions;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.MarketData.Ducascopy.FileSystem
{
    /// <summary>
    /// Ducascopy data downloader checker
    /// </summary>
    public class DataFileChecker
    {
        /// <summary>
        /// File system manager
        /// </summary>
        private readonly IFileManager<DucascopySymbol> _fileManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileManager">File system manager</param>
        public DataFileChecker(IFileManager<DucascopySymbol> fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public Task<IEnumerable<DateTime>> GetMissingDatesAsync(DucascopySymbol symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                return GetDesiredDates(start, end)
                .LeftJoin(_fileManager.GetDateTimes(symbol),
                    s => s,
                    l => new { Left = l, Right = (DateTime?)null },
                    (l, r) => new { Left = l, Right = (DateTime?)r })
                .Where(d => d.Right == null)
                .Select(d => d.Left);
            });
        }

        /// <summary>
        /// Get desired datetime list
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        protected IEnumerable<DateTime> GetDesiredDates(DateTime start, DateTime end)
        {
            var startFloor = start.FloorToHours();
            var hours = (end - startFloor).Hours;

            return Enumerable
                .Range(0, hours)
                .Select(i => startFloor.AddHours(i));
        }
    }
}
