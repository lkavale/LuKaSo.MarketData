using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models;
using LuKaSo.MarketData.Ducascopy.Infrastructure;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;

namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed
{
    /// <summary>
    /// Ducascopy configuration
    /// </summary>
    public class FileConfigurationReader : IConfigurationReader
    {
        /// <summary>
        /// Path to DucascopyDataFeedConfiguration
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileConfigurationReader() : this(Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileConfigurationReader)).Location))
        {
        }

        /// <summary>
        /// Constructor with specified path to config file
        /// </summary>
        /// <param name="path"></param>
        public FileConfigurationReader(string path)
        {
            _path = Path.Combine(path, "DucascopyDataFeedConfiguration.json");

            if (!File.Exists(_path))
            {
                throw new FileNotFoundException("Configuration file could not be found", _path);
            }
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public Configuration Read()
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<Configuration>(jsonReader);
            }
        }
    }
}
