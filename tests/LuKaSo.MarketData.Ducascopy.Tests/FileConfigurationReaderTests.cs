using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed;
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
            Assert.ThrowsException<FileNotFoundException>(() => new FileConfigurationReader("C:/"));
        }

        [TestMethod]
        public void TestInAssemblyPath()
        {
            var reader = new FileConfigurationReader();
            reader.Read();
        }
    }
}
