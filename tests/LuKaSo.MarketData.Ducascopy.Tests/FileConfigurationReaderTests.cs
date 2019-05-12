using LuKaSo.MarketData.Common.Downloader.DataFeed;
using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed;
using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace LuKaSo.MarketData.Ducascopy.Tests
{
    [TestClass]
    public class FileConfigurationReaderTests
    {
        [TestMethod]
        public void TestInvalidPath()
        {
            Assert.ThrowsException<FileNotFoundException>(() => new FileConfigurationReader<Configuration>("C:/", "DucascopyDataFeedConfiguration.json"));
        }

        [TestMethod]
        public void TestInAssemblyPath()
        {
            var reader = new FileConfigurationReader<Configuration>("DucascopyDataFeedConfiguration.json");
            reader.Read();
        }
    }
}
