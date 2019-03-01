using System;
using System.Threading.Tasks;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IFileDownloader
    {
        Task DownloadFileAync(Uri sourceFile, string destinationFile);
    }
}
