using CommandLine;
using LuKaSo.MarketData.Common.ProgressReporter;
using LuKaSo.MarketData.Ducascopy;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Pse;
using LuKaSo.MarketData.Types.Downloader;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lukaso.MarketData.Cli
{
    internal class Program
    {
        private static ServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            var configuration = new Configuration();
            configuration.DataPath = "C:/Data/PSE/";

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDucascopy(configuration)
                .AddPse(configuration)
                .BuildServiceProvider();

            _serviceProvider = serviceProvider;

            Parser.Default
                .ParseArguments<DownloaderOptions>(args)
                .WithParsed<DownloaderOptions>(o => RunDownload(o))
                .WithNotParsed<DownloaderOptions>(e => HandleError(e));

            Console.Read();
        }

        private static void RunDownload(DownloaderOptions options)
        {
            var item = options.CreateDownloaderItem();
            item.Status = DownloaderItemStatus.Ready;
            item.Indicator = new ConsoleProgressReporter(20, $"Downloading data for {options.Instrument}");

            var manager = _serviceProvider.GetServices<IDownloaderManager>()
                .Single(dm => dm.HasSymbol(item.Symbol));

            manager.Update(item);
            manager.Download(item);

        }

        private static void HandleError(IEnumerable<Error> errors)
        {
            Debugger.Break();
        }
    }
}
