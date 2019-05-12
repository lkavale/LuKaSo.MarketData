using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IDownloaderManager
    {
        bool HasSymbol(ISymbol symbol);
        void Update(IList<IDownloaderItem> downloaderItems);
        void Update(IDownloaderItem downloaderItem);
        void Download(IList<IDownloaderItem> downloaderItems);
        void Download(IDownloaderItem downloaderItem);
    }
}
