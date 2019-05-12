using LuKaSo.MarketData.Common.Extensions;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Pse.Instruments;
using LuKaSo.MarketData.Types.FileSystem;
using System;
using System.Collections.Specialized;

namespace LuKaSo.MarketData.Pse.FileSystem
{
    internal class PseFileNameGenerator : IFileNameGenerator<PseSymbol>
    {
        /// <summary>
        /// Base url
        /// </summary>
        private readonly Uri _baseUrl = new Uri("https://www.pse.cz/en/market-data/shares/historical-data/");

        /// <summary>
        /// Generate source file path
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GenerateSourceFileName(PseSymbol symbol, DateTime dateTime)
        {
            return new NameValueCollection() {
                    { "ISIN", symbol.Isin},
                    { "ID_NOTATION", symbol.NotationId},
                    { "c45275[DATETIME_TZ_START_RANGE]", "11/30/2012"},
                    { "c45275[DATETIME_TZ_END_RANGE]", "10/15/2018"},
                    { "c45275[DOWNLOAD]", "csv"}
                }.ToQueryString();
        }

        /// <summary>
        /// Generate destination file path
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GenerateDestinationFileName(PseSymbol symbol, DateTime dateTime)
        {
            return string.Format("{0}/{1}-{2}-{3}_data.csv",
                                symbol.Name,
                                dateTime.Year.ToString("D2"),
                                dateTime.Month.ToString("D2"),
                                dateTime.Day.ToString("D2"));
        }

        /// <summary>
        /// Generate file
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public IFile GenerateFile(PseSymbol symbol, DateTime dateTime)
        {
            return new File()
            {
                Time = dateTime,
                DestinationFile = GenerateDestinationFileName(symbol, dateTime),
                SourceFile = _baseUrl.Append(GenerateSourceFileName(symbol, dateTime))
            };
        }
    }
}
