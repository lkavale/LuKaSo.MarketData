using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace LuKaSo.MarketData.Ducascopy.Tests
{
    [TestClass]
    public class DataFeedConfigurationReaderTests
    {
        [TestMethod]
        public void TestInvalidPath()
        {
            Assert.ThrowsException<FileNotFoundException>(() => new ConfigurationReader("C:/"));
        }

        [TestMethod]
        public void TestInAssemblyPath()
        {
            var reader = new ConfigurationReader();
            reader.Read();
        }
    }
}
