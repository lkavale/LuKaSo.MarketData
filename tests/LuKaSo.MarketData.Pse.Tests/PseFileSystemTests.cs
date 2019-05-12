using LuKaSo.MarketData.Pse.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LuKaSo.MarketData.Pse.Tests
{
    internal class PseFileSystemWrapper : PseFileSystem
    {
        public PseFileSystemWrapper() : base(null)
        {
        }

        public bool TryGetDateTimeWrapper(string fileName, out DateTime? dateTime)
        {
            return TryGetDateTime(fileName, out dateTime);
        }
    }

    [TestClass]
    public class PseFileSystemTests
    {
        private PseFileSystemWrapper _pseFileSystemWrapper;

        [TestInitialize]
        public void Init()
        {
            _pseFileSystemWrapper = new PseFileSystemWrapper();
        }

        [TestMethod]
        public void TryGetDateTimeWrongFormat()
        {
            Assert.IsFalse(_pseFileSystemWrapper.TryGetDateTimeWrapper("YYYY-MM-DD_data.csv", out var dateTime));
        }

        [TestMethod]
        public void TryGetDateTimeWrongDateParameters()
        {
            Assert.IsFalse(_pseFileSystemWrapper.TryGetDateTimeWrapper("2018-13-31_data.csv", out var dateTime));
        }

        [TestMethod]
        public void TryGetDateTimeOk()
        {
            var dtIn = new DateTime(2018, 12, 31);

            Assert.IsTrue(_pseFileSystemWrapper.TryGetDateTimeWrapper($"{dtIn.Year}-{dtIn.Month}-{dtIn.Day}_data.csv", out var dtOut));
            Assert.AreEqual(dtIn, dtOut);
        }
    }
}
