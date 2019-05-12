using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Pse.Instruments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LuKaSo.MarketData.Pse.FileSystem
{
    public class PseFileSystem : IFileSystem<PseSymbol>
    {
        /// <summary>
        /// Base directory
        /// </summary>
        protected IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public PseFileSystem(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get datetimes
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetDateTimes(PseSymbol symbol)
        {
            var instrumentDirectory = Path.Combine(_configuration.DataPath, symbol.Name);

            foreach (var fileName in GetFiles(instrumentDirectory))
            {
                if (TryGetDateTime(fileName, out DateTime? dateTime))
                {
                    yield return (DateTime)dateTime;
                }

                continue;
            }
        }

        /// <summary>
        /// Get end datetime
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public DateTime GetEndDateTime(PseSymbol symbol)
        {
            return GetDateTimes(symbol).Max();
        }

        /// <summary>
        /// Get start datetime
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public DateTime GetStartDateTime(PseSymbol symbol)
        {
            return GetDateTimes(symbol).Min();
        }

        /// <summary>
        /// Try get datetime from filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        protected bool TryGetDateTime(string fileName, out DateTime? dateTime)
        {
            var match = Regex.Match(fileName, @"([0-9]+)-([0-9]+)-([0-9]+)_data.csv$", RegexOptions.IgnoreCase);
            dateTime = null;

            if (match.Success)
            {
                int year = int.Parse(match.Groups[1].Value);
                int month = int.Parse(match.Groups[2].Value);
                int day = int.Parse(match.Groups[3].Value);

                try
                {
                    dateTime = new DateTime(year, month, day);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Get files
        /// </summary>
        /// <param name="baseDirectory">Directory base</param>
        /// <returns>File names</returns>
        protected IEnumerable<string> GetFiles(string baseDirectory)
        {
            if (!Directory.Exists(baseDirectory))
            {
                throw new ArgumentException("Given base directory is not exists.");
            }

            foreach (var file in Directory.GetFiles(baseDirectory))
            {
                yield return Path.GetFileName(file);
            }
        }
    }
}
