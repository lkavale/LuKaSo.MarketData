using LuKaSo.MarketData.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace LuKaSo.MarketData.Common.Tests
{
    [TestClass]
    public class DirectoryHelpersTests
    {
        private string _path;

        [TestInitialize]
        public void Init()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileDownloaderTests)).Location);
            _path = Path.Combine(assemblyPath, "DirectoryHelpersTests");

            if (Directory.Exists(_path))
            {
                Directory.Delete(_path, true);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete(_path, true);
        }

        [TestMethod]
        public void DirectoryCreateIfNotExistOk()
        {
            DirectoryHelpers.DirectoryCreateIfNotExist(_path);
            Assert.IsTrue(Directory.Exists(_path));
        }
    }
}
