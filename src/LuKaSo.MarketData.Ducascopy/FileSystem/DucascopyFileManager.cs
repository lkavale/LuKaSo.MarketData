using LuKaSo.MarketData.Common.Extensions;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.FileSystem
{
    /// <summary>
    /// Ducascopy data downloader checker
    /// </summary>
    public class DucascopyFileManager : IFileManager<DucascopySymbol>
    {
        /// <summary>
        /// File system manager
        /// </summary>
        private readonly IFileSystem<DucascopySymbol> _fileManager;

        /// <summary>
        /// File name generator
        /// </summary>
        private readonly IFileNameGenerator<DucascopySymbol> _fileNameGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileManager">File system manager</param>
        /// <param name="fileNameGenerator">File name generator</param>
        public DucascopyFileManager(IFileSystem<DucascopySymbol> fileManager, IFileNameGenerator<DucascopySymbol> fileNameGenerator)
        {
            _fileManager = fileManager;
            _fileNameGenerator = fileNameGenerator;
        }

        /// <summary>
        /// Get missing files for symbol
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
                    return _fileNameGenerator.GenerateFile(symbol, d);
                });
        }

        /// <summary>
        /// Get date and times of missing files
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
        /// Get exisiting files for symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<IFile> GetExistingFiles(DucascopySymbol symbol, DateTime start, DateTime end)
        {
            return _fileManager.GetDateTimes(symbol)
                .Select(d =>
                {
                    return _fileNameGenerator.GenerateFile(symbol, d);
                });
        }

        /// <summary>
        /// Get date and times of existing files
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetExistingDates(DucascopySymbol symbol, DateTime start, DateTime end)
        {
            return _fileManager.GetDateTimes(symbol);
        }

        /// <summary>
        /// Get desired datetime list
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> GetDesiredDates(DateTime start, DateTime end)
        {
            var startFloor = start.FloorToHours();
            var hours = (int)(end.CeilToHours() - startFloor).TotalHours;

            return Enumerable
                .Range(0, hours)
                .Select(i => startFloor.AddHours(i));
        }
    }
}
