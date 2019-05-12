using LuKaSo.MarketData.Common.Extensions;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Pse.Instruments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Pse.FileSystem
{
    public class PseFileManager : IFileManager<PseSymbol>
    {
        /// <summary>
        /// File system manager
        /// </summary>
        private readonly IFileSystem<PseSymbol> _fileManager;

        /// <summary>
        /// File name generator
        /// </summary>
        private readonly IFileNameGenerator<PseSymbol> _fileNameGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileManager">File system manager</param>
        /// <param name="fileNameGenerator">File name generator</param>
        public PseFileManager(IFileSystem<PseSymbol> fileManager, IFileNameGenerator<PseSymbol> fileNameGenerator)
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
        public IEnumerable<IFile> GetMissingFiles(PseSymbol symbol, DateTime start, DateTime end)
        {
            return GetMissingDates(symbol, start, end)
                .Select(d => _fileNameGenerator.GenerateFile(symbol, d));
        }

        /// <summary>
        /// Get date and times of missing files
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetMissingDates(PseSymbol symbol, DateTime start, DateTime end)
        {
            var dateTimes = new List<DateTime>();

            if (!_fileManager.GetDateTimes(symbol).Contains(end.FloorToDays()))
            {
                dateTimes.Add(end);
            }

            return dateTimes;
        }

        /// <summary>
        /// Get exisiting files for symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<IFile> GetExistingFiles(PseSymbol symbol, DateTime start, DateTime end)
        {
            return _fileManager.GetDateTimes(symbol)
                .Select(d => _fileNameGenerator.GenerateFile(symbol, d));
        }

        /// <summary>
        /// Get date and times of existing files
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetExistingDates(PseSymbol symbol, DateTime start, DateTime end)
        {
            return _fileManager.GetDateTimes(symbol);
        }
    }
}
