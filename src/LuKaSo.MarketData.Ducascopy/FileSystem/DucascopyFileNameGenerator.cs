using LuKaSo.MarketData.Common.Extensions;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Types.FileSystem;
using System;

namespace LuKaSo.MarketData.Ducascopy.FileSystem
{
    internal class DucascopyFileNameGenerator : IFileNameGenerator<DucascopySymbol>
    {
        /// <summary>
        /// Base url
        /// </summary>
        private readonly Uri _baseUrl = new Uri("http://www.dukascopy.com/datafeed/");

        /// <summary>
        /// Generate source file path
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GenerateSourceFileName(DucascopySymbol symbol, DateTime dateTime)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}h_ticks.bi5",
                                symbol.DirectoryName,
                                dateTime.Year.ToString("D2"),
                                (dateTime.Month - 1).ToString("D2"),
                                dateTime.Day.ToString("D2"),
                                dateTime.Hour.ToString("D2"));
        }

        /// <summary>
        /// Generate destination file path
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GenerateDestinationFileName(DucascopySymbol symbol, DateTime dateTime)
        {
            return GenerateSourceFileName(symbol, dateTime);
        }

        /// <summary>
        /// Generate file
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public IFile GenerateFile(DucascopySymbol symbol, DateTime dateTime)
        {
            var fileName = GenerateSourceFileName(symbol, dateTime);

            return new File()
            {
                Time = dateTime,
                DestinationFile = fileName,
                SourceFile = _baseUrl.Append(fileName)
            };
        }
    }
}
