using LuKaSo.MarketData.Common.Downloader;
using LuKaSo.MarketData.Ducascopy.Downloader;
using LuKaSo.MarketData.Ducascopy.FileSystem;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace LuKaSo.MarketData.Ducascopy
{
    public static class DucascopyComposition
    {
        public static IServiceCollection AddDucascopy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IFileNameGenerator<DucascopySymbol>, DucascopyFileNameGenerator>();
            services.AddScoped<IFileManager<DucascopySymbol>, DucascopyFileManager>();
            services.AddScoped<IFileChecker<DucascopySymbol>, DucascopyFileChecker>();
            services.AddScoped<IDownloaderManager, DucascopyDownloaderManager>();
            services.AddScoped<IFileDownloader, FileDownloader>();

            return services;
        }
    }
}
