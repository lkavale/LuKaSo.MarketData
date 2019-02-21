using System.Threading.Tasks;

namespace LuKaSo.MarketData.Infrastructure.Downloader
{
    public interface IFileDownloader
    {
        Task DownloadFileAync(string sourceFile, string destinationFile);
    }
}
