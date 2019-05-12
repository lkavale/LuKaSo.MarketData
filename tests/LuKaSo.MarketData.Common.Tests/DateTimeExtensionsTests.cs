using LuKaSo.MarketData.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LuKaSo.MarketData.Common.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void FloorToHoursExactlyTheSame()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 0);
            var dateFloored = date.FloorToHours();

            Assert.AreEqual(date, dateFloored);
        }

        [TestMethod]
        public void FloorToHours()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 1);
            var dateFloored = date.FloorToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 1, 0, 0), dateFloored);

            date = new DateTime(2000, 1, 1, 1, 59, 59);
            dateFloored = date.FloorToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 1, 0, 0), dateFloored);
        }

        [TestMethod]
        public void CeilToHoursExactlyTheSame()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 0);
            var dateCeiled = date.CeilToHours();

            Assert.AreEqual(date, dateCeiled);
        }

        [TestMethod]
        public void CeilToHours()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 1);
            var dateCeiled = date.CeilToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 2, 0, 0), dateCeiled);

            date = new DateTime(2000, 1, 1, 1, 59, 59);
            dateCeiled = date.CeilToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 2, 0, 0), dateCeiled);
        }

        [TestMethod]
        public void FloorToDaysExactlyTheSame()
        {
            var date = new DateTime(2000, 1, 1, 0, 0, 0);
            var dateFloored = date.FloorToDays();

            Assert.AreEqual(date, dateFloored);
        }

        [TestMethod]
        public void FloorToDays()
        {
            var date = new DateTime(2000, 1, 1, 0, 0, 1);
            var dateFloored = date.FloorToDays();

            Assert.AreEqual(new DateTime(2000, 1, 1, 0, 0, 0), dateFloored);

            date = new DateTime(2000, 1, 1, 23, 59, 59);
            dateFloored = date.FloorToDays();

            Assert.AreEqual(new DateTime(2000, 1, 1, 0, 0, 0), dateFloored);
        }

        [TestMethod]
        public void CeilToDaysExactlyTheSame()
        {
            var date = new DateTime(2000, 1, 1, 0, 0, 0);
            var dateCeiled = date.CeilToDays();

            Assert.AreEqual(date, dateCeiled);
        }

        [TestMethod]
        public void CeilToDays()
        {
            var date = new DateTime(2000, 1, 1, 0, 0, 1);
            var dateCeiled = date.CeilToDays();

            Assert.AreEqual(new DateTime(2000, 1, 2, 0, 0, 0), dateCeiled);

            date = new DateTime(2000, 1, 1, 23, 59, 59);
            dateCeiled = date.CeilToDays();

            Assert.AreEqual(new DateTime(2000, 1, 2, 0, 0, 0), dateCeiled);
        }
    }
}
