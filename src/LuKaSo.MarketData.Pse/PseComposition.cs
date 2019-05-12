using LuKaSo.MarketData.Common.Downloader;
using LuKaSo.MarketData.Common.Downloader.DataFeed;
using LuKaSo.MarketData.Common.Instruments;
using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.Downloader.DataFeed;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Infrastructure.Instruments;
using LuKaSo.MarketData.Pse.Downloader;
using LuKaSo.MarketData.Pse.Downloader.DataFeed;
using LuKaSo.MarketData.Pse.Downloader.DataFeed.Models;
using LuKaSo.MarketData.Pse.FileSystem;
using LuKaSo.MarketData.Pse.Instruments;
using Microsoft.Extensions.DependencyInjection;

namespace LuKaSo.MarketData.Pse
{
    public static class PseComposition
    {
        public static IServiceCollection AddPse(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IConfigurationReader<Configuration>, FileConfigurationReader<Configuration>>((sp) => new FileConfigurationReader<Configuration>("PseDataFeedConfiguration.json"));
            services.AddSingleton<IDataFeedConfiguration<PseSymbol, PseGroup>, PseDataFeedConfiguration>();
            services.AddSingleton<IInstrumentManager<PseSymbol, PseGroup>, InstrumentManager<PseSymbol, PseGroup>>();
            services.AddSingleton<IFileNameGenerator<PseSymbol>, PseFileNameGenerator>();
            services.AddScoped<IFileSystem<PseSymbol>, PseFileSystem>();
            services.AddScoped<IFileManager<PseSymbol>, PseFileManager>();
            services.AddScoped<IDownloaderManager, PseDownloadManager>();
            services.AddScoped<IFileDownloader, FileDownloader>();

            return services;
        }
    }
}
