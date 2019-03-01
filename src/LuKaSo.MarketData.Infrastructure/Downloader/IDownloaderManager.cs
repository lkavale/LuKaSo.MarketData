using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IDownloaderManager
    {
        void Update(IList<IDownloaderItem> downloaderItems);
        void Update(IDownloaderItem downloaderItem);
        void Download(IList<IDownloaderItem> downloaderItems);
        void Download(IDownloaderItem downloaderItem);
    }
}
