using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models;

namespace LuKaSo.MarketData.Ducascopy.Infrastructure
{
    public interface IConfigurationReader
    {
        Configuration Read();
    }
}
