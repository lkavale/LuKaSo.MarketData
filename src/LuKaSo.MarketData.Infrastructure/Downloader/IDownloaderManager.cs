using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IDownloaderManager
    {
        void UpdateDownloaderStatus(IList<IDownloaderItem> downloaderItems);

        //void Synchronize

    }
}
