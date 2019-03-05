using CommandLine;
using LuKaSo.MarketData.Common.ProgressReporter;
using LuKaSo.MarketData.Ducascopy;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Types.Downloader;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lukaso.MarketData.Cli
{
    internal class Program
    {
        private static ServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDucascopy(new Configuration())
                .BuildServiceProvider();

            _serviceProvider = serviceProvider;

            Parser.Default
                .ParseArguments<DownloaderOption>(args)
                .WithParsed<DownloaderOption>(o => RunDownload(o))
                .WithNotParsed<DownloaderOption>(e => HandleError(e));

            Console.Read();
        }

        private static void RunDownload(DownloaderOption options)
        {
            Console.WriteLine($"Downloading data for {options.Instrument}: ");

            var item = options.CreateDownloaderItem();
            item.Status = DownloaderItemStatus.Ready;
            item.Indicator = new ConsoleProgressReporter();

            var manager = _serviceProvider.GetService<IDownloaderManager>();

            manager.Update(item);
            manager.Download(item);

            Console.WriteLine($"Download completed.");
        }

        private static void HandleError(IEnumerable<Error> errors)
        {
            Debugger.Break();
        }
    }
}
