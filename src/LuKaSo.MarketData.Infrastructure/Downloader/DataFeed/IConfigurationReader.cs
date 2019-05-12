namespace LuKaSo.MarketData.Infrastructure.Downloader.DataFeed
{
    public interface IConfigurationReader<T>
    {
        T Read();
    }
}
