using LuKaSo.MarketData.Common.Extensions;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Types.FileSystem;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.FileSystem
{
    /// <summary>
    /// Ducascopy data downloader checker
    /// </summary>
    public class DucascopyFileChecker : IFileChecker<DucascopySymbol>
    {
        /// <summary>
        /// File system manager
        /// </summary>
        private readonly IFileManager<DucascopySymbol> _fileManager;

        /// <summary>
        /// File name generator
        /// </summary>
        private readonly IFileNameGenerator<DucascopySymbol> _fileNameGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileManager">File system manager</param>
        public DucascopyFileChecker(IFileManager<DucascopySymbol> fileManager, IFileNameGenerator<DucascopySymbol> fileNameGenerator)
        {
            _fileManager = fileManager;
            _fileNameGenerator = fileNameGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<IFile> GetMissingFiles(DucascopySymbol symbol, DateTime start, DateTime end)
        {
            return GetMissingDates(symbol, start, end)
                .Select(d =>
                {
                    var fileName = _fileNameGenerator.GenerateFileName(symbol, d);
                    return new File()
                    {
                        Time = d,
                        DestinationFile = fileName,
                        SourceFile = fileName
                    };
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetMissingDates(DucascopySymbol symbol, DateTime start, DateTime end)
        {
            return GetDesiredDates(start, end)
                .LeftJoin(_fileManager.GetDateTimes(symbol),
                    s => s,
                    l => new { Left = l, Right = (DateTime?)null },
                    (l, r) => new { Left = l, Right = (DateTime?)r })
                .Where(d => d.Right == null)
                .Select(d => d.Left);
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
            var hours = (int)(end.CeilToHours() - startFloor).TotalHours;

            return Enumerable
                .Range(0, hours)
                .Select(i => startFloor.AddHours(i));
        }
    }
}
