using CommandLine;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Types.Downloader;
using System;
using System.Globalization;

namespace Lukaso.MarketData.Cli
{
    [Verb("download", HelpText = "Download specified instrument market data.")]
    public class DownloaderOption
    {
        [Option('i', "instrument", HelpText = "Instrument symbol.")]
        public string Instrument { get; set; }

        [Option('f', "from", HelpText = "Start date and time (YYYY-MM-DD).")]
        public string DateFrom { get; set; }

        [Option('t', "to", HelpText = "End date and time (YYYY-MM-DD).")]
        public string DateTo { get; set; }

        public DownloaderItem CreateDownloaderItem()
        {
            return new DownloaderItem()
            {
                Symbol = new DucascopySymbol() { Name = Instrument, DirectoryName = Instrument },
                DateFromDesired = DateTime.ParseExact(DateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateToDesired = DateTime.ParseExact(DateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
        }
    }
}
