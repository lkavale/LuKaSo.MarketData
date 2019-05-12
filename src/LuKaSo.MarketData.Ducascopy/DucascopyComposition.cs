using LuKaSo.MarketData.Common.Downloader;
using LuKaSo.MarketData.Common.Downloader.DataFeed;
using LuKaSo.MarketData.Common.Instruments;
using LuKaSo.MarketData.Ducascopy.Downloader;
using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed;
using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models;
using LuKaSo.MarketData.Ducascopy.FileSystem;
using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.Downloader.DataFeed;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Infrastructure.Instruments;
using Microsoft.Extensions.DependencyInjection;

namespace LuKaSo.MarketData.Ducascopy
{
    public static class DucascopyComposition
    {
        public static IServiceCollection AddDucascopy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IConfigurationReader<Configuration>, FileConfigurationReader<Configuration>>((sp) => new FileConfigurationReader<Configuration>("DucascopyDataFeedConfiguration.json"));
            services.AddSingleton<IDataFeedConfiguration<DucascopySymbol, DucascopyGroup>, DucacopyDataFeedConfiguration>();
            services.AddSingleton<IInstrumentManager<DucascopySymbol, DucascopyGroup>, InstrumentManager<DucascopySymbol, DucascopyGroup>>();
            services.AddSingleton<IFileNameGenerator<DucascopySymbol>, DucascopyFileNameGenerator>();
            services.AddScoped<IFileSystem<DucascopySymbol>, DucascopyFileSystem>();
            services.AddScoped<IFileManager<DucascopySymbol>, DucascopyFileManager>();
            services.AddScoped<IDownloaderManager, DucascopyDownloaderManager>();
            services.AddScoped<IFileDownloader, FileDownloader>();

            return services;
        }
    }
}
