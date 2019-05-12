using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Infrastructure.Instruments;
using LuKaSo.MarketData.Pse.Instruments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuKaSo.MarketData.Pse.Downloader
{
    public class PseDownloadManager : IDownloaderManager
    {
        private readonly IFileManager<PseSymbol> _fileManager;
        private readonly IFileDownloader _fileDownloader;
        private readonly IConfiguration _configuration;
        private readonly IInstrumentManager<PseSymbol, PseGroup> _instrumentManager;

        public PseDownloadManager(IFileManager<PseSymbol> fileManager, IFileDownloader fileDownloader, IConfiguration configuration, IInstrumentManager<PseSymbol, PseGroup> instrumentManager)
        {
            _fileManager = fileManager;
            _fileDownloader = fileDownloader;
            _configuration = configuration;
            _instrumentManager = instrumentManager;
        }

        public bool HasSymbol(ISymbol symbol)
        {
            return symbol is PseSymbol;
        }

        public void Download(IList<IDownloaderItem> downloaderItems)
        {
            downloaderItems
                .Where(d => d.Symbol is PseSymbol && d.Files.Any())
                .ToList()
                .ForEach(d =>
                {
                    Download(d);
                });
        }

        public void Download(IDownloaderItem downloaderItem)
        {
            int i = 0;
            downloaderItem.Indicator.Start(downloaderItem.Files.Count());

            foreach (var file in downloaderItem.Files)
            {
                _fileDownloader.DownloadFileAync(file.SourceFile, Path.Combine(_configuration.DataPath, file.DestinationFile)).GetAwaiter().GetResult();
                downloaderItem.Indicator.Report(++i);
            }

            downloaderItem.Indicator.Finish();
        }

        public void Update(IList<IDownloaderItem> downloaderItems)
        {
            downloaderItems
                .Where(d => d.Symbol is PseSymbol && !(d.DateFromDesired == null && d.DateToDesired == null))
                .ToList()
                .ForEach(d =>
                {
                    d.Files = _fileManager.GetMissingFiles((PseSymbol)d.Symbol, d.DateFromDesired ?? d.DateFrom, d.DateToDesired ?? d.DateTo);
                });
        }

        public void Update(IDownloaderItem downloaderItem)
        {
            CheckedForAvailability(downloaderItem);

            downloaderItem.Files = _fileManager.GetMissingFiles((PseSymbol)downloaderItem.Symbol, downloaderItem.DateFromDesired ?? downloaderItem.DateFrom, downloaderItem.DateToDesired ?? downloaderItem.DateTo);
        }

        public void CheckedForAvailability(IDownloaderItem downloaderItem)
        {
            if (!_instrumentManager.IsSymbolExists(downloaderItem.Symbol.Name))
            {
                throw new ArgumentException($"PSE does not provide data for symbol {downloaderItem.Symbol.Name}");
            }

            downloaderItem.Symbol = _instrumentManager.GetSymbolByName(downloaderItem.Symbol.Name);
        }
    }
}
