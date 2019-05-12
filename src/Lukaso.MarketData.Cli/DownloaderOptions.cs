using CommandLine;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Pse.Instruments;
using LuKaSo.MarketData.Types.Downloader;
using System;
using System.Globalization;

namespace Lukaso.MarketData.Cli
{
    [Verb("download", HelpText = "Download specified instrument market data.")]
    public class DownloaderOptions
    {
        [Option('s', "source", HelpText = "Data source.")]
        public string Source { get; set; }

        [Option('i', "instrument", HelpText = "Instrument symbol.")]
        public string Instrument { get; set; }

        [Option('f', "from", HelpText = "Start date and time (YYYY-MM-DD).")]
        public string DateFrom { get; set; }

        [Option('t', "to", HelpText = "End date and time (YYYY-MM-DD).")]
        public string DateTo { get; set; }

        public DownloaderItem CreateDownloaderItem()
        {
            var item = new DownloaderItem()
            {
                DateFromDesired = DateTime.ParseExact(DateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateToDesired = DateTime.ParseExact(DateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };

            if (Source == "Ducascopy")
            {
                item.Symbol = new DucascopySymbol() { Name = Instrument, DirectoryName = Instrument };
            }

            if (Source == "Pse")
            {
                item.Symbol = new PseSymbol() { Name = Instrument};
            }

            return item;
        }
    }
}
