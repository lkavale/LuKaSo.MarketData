using LuKaSo.MarketData.Common.Downloader;
using LuKaSo.MarketData.Common.Extensions;
using LuKaSo.MarketData.Common.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LuKaSo.MarketData.Common.Tests
{
    [TestClass]
    public class FileDownloaderTests
    {
        private string _path;
        private ILogger<FileDownloader> _logger;
        private FileDownloader _downloader;

        [TestInitialize]
        public void Init()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileDownloaderTests)).Location);
            _path = Path.Combine(assemblyPath, "DownloaderTests");

            if (Directory.Exists(_path))
            {
                Directory.Delete(_path, true);
            }

            DirectoryHelpers.DirectoryCreateIfNotExist(_path);

            _logger = Mock.Of<ILogger<FileDownloader>>();
            _downloader = new FileDownloader(_logger);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete(_path, true);
        }

        [TestMethod]
        public async Task FileDownloaderOk()
        {
            var file = "00h_ticks.bi5";
            var source = new Uri("http://www.dukascopy.com/datafeed/EURUSD/2018/11/31/").Append(file);
            var destination = Path.Combine(_path, file);

            await _downloader.DownloadFileAync(source, destination);

            var files = Directory.GetFiles(_path);
            Assert.IsTrue(files.Contains(destination));
        }

        [TestMethod]
        public async Task FileDownloaderNotExists()
        {
            var file = "00h_ticks.bi5";
            var source = new Uri("http://www.dukascopy.com/datafeed/EURUSD/2018/12/31/").Append(file);
            var destination = Path.Combine(_path, file);

            await Assert.ThrowsExceptionAsync<WebException>(async () => await _downloader.DownloadFileAync(source, destination));

            var files = Directory.GetFiles(_path);
            Assert.IsFalse(files.Contains(destination));
        }
    }
}
