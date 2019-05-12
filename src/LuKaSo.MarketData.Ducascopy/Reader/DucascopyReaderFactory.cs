using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using System;

namespace LuKaSo.MarketData.Ducascopy.Reader
{
    public class DucascopyReaderFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IFileManager<DucascopySymbol> _fileManager;

        public DucascopyReaderFactory(IConfiguration configuration, IFileManager<DucascopySymbol> fileManager)
        {
            _configuration = configuration;
            _fileManager = fileManager;
        }

        public DucascopyReader Create(DucascopySymbol symbol, DateTime? timeFrom, DateTime timeTo)
        {
            return new DucascopyReader(_configuration, _fileManager, symbol, timeFrom, timeTo);
        }
    }
}
