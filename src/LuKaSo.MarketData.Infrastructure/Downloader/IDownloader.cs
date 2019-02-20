using LuKaSo.MarketData.Types.Downloader;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IDataDownloader
    {
        Task<bool> DownloadAsync(DownloaderItem downloaderItem, CancellationToken cancellationToken);
    }
}
