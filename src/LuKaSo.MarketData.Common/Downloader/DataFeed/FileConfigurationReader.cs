using LuKaSo.MarketData.Infrastructure.Downloader.DataFeed;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;

namespace LuKaSo.MarketData.Common.Downloader.DataFeed
{
    /// <summary>
    /// Datafeed configuration file reader
    /// </summary>
    public class FileConfigurationReader<T> : IConfigurationReader<T>
    {
        /// <summary>
        /// Path to configuration file
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileConfigurationReader(string fileName) : this(Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileConfigurationReader<>)).Location), fileName)
        {
        }

        /// <summary>
        /// Constructor with specified path to config file
        /// </summary>
        /// <param name="path"></param>
        public FileConfigurationReader(string path, string fileName)
        {
            _path = Path.Combine(path, fileName);

            if (!File.Exists(_path))
            {
                throw new FileNotFoundException("Configuration file could not be found", _path);
            }
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public T Read()
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
