using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.FileSystem
{
    public class DucascopyFileManager : IFileManager<DucascopySymbol>
    {
        /// <summary>
        /// Base directory
        /// </summary>
        protected IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseDirectory"></param>
        public DucascopyFileManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Start time of data
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public DateTime GetStartDateTime(DucascopySymbol symbol)
        {
            var instrumentDirectory = Path.Combine(_configuration.DataPath, symbol.Name);

            var year = GetSubdirectories(instrumentDirectory)
                .Where(d => int.TryParse(d, out int dYear) && dYear > 1900 && dYear < 2100)
                .Min(d => int.Parse(d));

            var month = GetSubdirectories(Path.Combine(instrumentDirectory, year.ToString()))
                .Where(d => int.TryParse(d, out int dMonth) && dMonth >= 0 && dMonth <= 11)
                .Min(d => int.Parse(d)) + 1;

            var day = GetSubdirectories(Path.Combine(instrumentDirectory, year.ToString(), month.ToString()))
                .Where(d => int.TryParse(d, out int dDay))
                .Min(d => int.Parse(d));

            var hour = GetFiles(Path.Combine(instrumentDirectory, year.ToString(), month.ToString(), day.ToString()))
                .Where(f => int.TryParse(f.Substring(0, 2), out int dHour) && dHour >= 0 && dHour <= 23)
                .Min(f => int.Parse(f.Substring(0, 2)));

            return new DateTime(year, month, day, hour, 0, 0);
        }

        /// <summary>
        /// End time of data
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public DateTime GetEndDateTime(DucascopySymbol symbol)
        {
            var instrumentDirectory = Path.Combine(_configuration.DataPath, symbol.Name);

            var year = GetSubdirectories(instrumentDirectory)
                .Where(d => int.TryParse(d, out int dYear) && dYear > 1900 && dYear < 2100)
                .Max(d => int.Parse(d));

            var month = GetSubdirectories(Path.Combine(instrumentDirectory, year.ToString()))
                .Where(d => int.TryParse(d, out int dMonth) && dMonth >= 0 && dMonth <= 11)
                .Max(d => int.Parse(d)) + 1;

            var day = GetSubdirectories(Path.Combine(instrumentDirectory, year.ToString(), month.ToString()))
                .Where(d => int.TryParse(d, out int dDay))
                .Max(d => int.Parse(d));

            var hour = GetFiles(Path.Combine(instrumentDirectory, year.ToString(), month.ToString(), day.ToString()))
                .Where(f => int.TryParse(f.Substring(0, 2), out int dHour) && dHour >= 0 && dHour <= 23)
                .Max(f => int.Parse(f.Substring(0, 2)));

            return new DateTime(year, month, day, hour, 0, 0);
        }

        /// <summary>
        /// Get all dates of data
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetDateTimes(DucascopySymbol symbol)
        {
            var instrumentDirectory = Path.Combine(_configuration.DataPath, symbol.Name);

            return ResolveYears(instrumentDirectory);
        }

        #region File system methods

        /// <summary>
        /// Resolve years
        /// </summary>
        /// <param name="instrumentDirectory">Instrument directory</param>
        /// <returns>Date time list</returns>
        protected IEnumerable<DateTime> ResolveYears(string instrumentDirectory)
        {
            return GetSubdirectories(instrumentDirectory)
                .Where(d => int.TryParse(d, out int year) && year > 1900 && year < 2100)
                .SelectMany(d =>
                {
                    var year = int.Parse(d);
                    return ResolveMonths(Path.Combine(instrumentDirectory, d), year);
                });
        }

        /// <summary>
        /// Resolve month
        /// </summary>
        /// <param name="yearDirectory">Year directory</param>
        /// <param name="year">Year</param>
        /// <returns>Date time list</returns>
        protected IEnumerable<DateTime> ResolveMonths(string yearDirectory, int year)
        {
            return GetSubdirectories(yearDirectory)
                .Where(d => int.TryParse(d, out int month) && month >= 0 && month <= 11)
                .SelectMany(d =>
                {
                    var month = int.Parse(d);
                    return ResolveDays(Path.Combine(yearDirectory, d), year, month + 1);
                });
        }

        /// <summary>
        /// Resolve days
        /// </summary>
        /// <param name="monthDirectory">Month directory</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns>Date time list</returns>
        protected IEnumerable<DateTime> ResolveDays(string monthDirectory, int year, int month)
        {
            return GetSubdirectories(monthDirectory)
                .Where(d => int.TryParse(d, out int day))
                .SelectMany(d =>
                {
                    var day = int.Parse(d);
                    return ResolveHours(Path.Combine(monthDirectory, d), year, month, day);
                });
        }

        /// <summary>
        /// Resolve hours from filename
        /// </summary>
        /// <param name="dayDirectory">Day directory</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <returns>Date time list</returns>
        protected IEnumerable<DateTime> ResolveHours(string dayDirectory, int year, int month, int day)
        {
            return GetFiles(dayDirectory)
                .Where(f => int.TryParse(f.Substring(0, 2), out int hour) && hour >= 0 && hour <= 23)
                .Select(f =>
                {
                    var hour = int.Parse(f.Substring(0, 2));
                    return new DateTime(year, month, day, hour, 0, 0);
                });
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

        /// <summary>
        /// Get subdirectories
        /// </summary>
        /// <param name="baseDirectory">Directory base</param>
        /// <returns>Directory names</returns>
        protected IEnumerable<string> GetSubdirectories(string baseDirectory)
        {
            if (!Directory.Exists(baseDirectory))
            {
                return new List<string>();
            }

            return Directory.GetDirectories(baseDirectory)
                .Select(d =>
                {
                    var info = new DirectoryInfo(d);
                    return info.Name;
                });
        }

        #endregion
    }
}
