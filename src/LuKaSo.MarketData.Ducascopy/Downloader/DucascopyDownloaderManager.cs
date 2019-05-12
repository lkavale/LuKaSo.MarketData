﻿using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.Downloader
{
    public class DucascopyDownloaderManager : IDownloaderManager
    {
        private readonly IFileManager<DucascopySymbol> _fileManager;
        private readonly IFileDownloader _fileDownloader;
        private readonly IConfiguration _configuration;
        private readonly IInstrumentManager<DucascopySymbol, DucascopyGroup> _instrumentManager;

        public DucascopyDownloaderManager(IFileManager<DucascopySymbol> fileManager, IFileDownloader fileDownloader, IConfiguration configuration, IInstrumentManager<DucascopySymbol, DucascopyGroup> instrumentManager)
        {
            _fileManager = fileManager;
            _fileDownloader = fileDownloader;
            _configuration = configuration;
            _instrumentManager = instrumentManager;
        }

        public bool HasSymbol(ISymbol symbol)
        {
            return symbol is DucascopySymbol;
        }

        public void Update(IList<IDownloaderItem> downloaderItems)
        {
            downloaderItems
                .Where(d => d.Symbol is DucascopySymbol && !(d.DateFromDesired == null && d.DateToDesired == null))
                .ToList()
                .ForEach(d =>
                {
                    d.Files = _fileManager.GetMissingFiles((DucascopySymbol)d.Symbol, d.DateFromDesired ?? d.DateFrom, d.DateToDesired ?? d.DateTo);
                });
        }

        public void Update(IDownloaderItem downloaderItem)
        {
            CheckedForAvailability(downloaderItem);

            downloaderItem.Files = _fileManager.GetMissingFiles((DucascopySymbol)downloaderItem.Symbol, downloaderItem.DateFromDesired ?? downloaderItem.DateFrom, downloaderItem.DateToDesired ?? downloaderItem.DateTo);
        }

        public void Download(IList<IDownloaderItem> downloaderItems)
        {
            downloaderItems
                .Where(d => d.Symbol is DucascopySymbol && d.Files.Any())
                .ToList()
                .ForEach(d =>
                {
                    Download(d);
                });
        }

        public void Download(IDownloaderItem downloaderItem)
        {
            CheckedForAvailability(downloaderItem);

            int i = 0;
            downloaderItem.Indicator.Start(downloaderItem.Files.Count());

            foreach (var file in downloaderItem.Files)
            {
                _fileDownloader.DownloadFileAync(file.SourceFile, Path.Combine(_configuration.DataPath, file.DestinationFile)).GetAwaiter().GetResult();
                downloaderItem.Indicator.Report(++i);
            }

            downloaderItem.Indicator.Finish();
        }

        public void CheckedForAvailability(IDownloaderItem downloaderItem)
        {
            if (!_instrumentManager.IsSymbolExists(downloaderItem.Symbol.Name))
            {
                throw new ArgumentException($"Ducascopy does not provide data for symbol {downloaderItem.Symbol.Name}");
            }
        }
    }
}
