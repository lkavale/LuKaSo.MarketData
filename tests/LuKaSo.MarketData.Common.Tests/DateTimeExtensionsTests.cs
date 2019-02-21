using LuKaSo.MarketData.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LuKaSo.MarketData.Common.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        public void FloorToHoursExactlyTheSame()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 0);
            var dateFloored = date.FloorToHours();

            Assert.AreEqual(date, dateFloored);
        }

        public void FloorToHours()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 1);
            var dateFloored = date.FloorToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 1, 0, 0), dateFloored);

            date = new DateTime(2000, 1, 1, 1, 59, 59);
            dateFloored = date.FloorToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 1, 0, 0), dateFloored);
        }

        public void CeilToHoursExactlyTheSame()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 0);
            var dateFloored = date.CeilToHours();

            Assert.AreEqual(date, dateFloored);
        }

        public void CeilToHours()
        {
            var date = new DateTime(2000, 1, 1, 1, 0, 1);
            var dateFloored = date.CeilToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 2, 0, 0), dateFloored);

            date = new DateTime(2000, 1, 1, 1, 59, 59);
            dateFloored = date.CeilToHours();

            Assert.AreEqual(new DateTime(2000, 1, 1, 2, 0, 0), dateFloored);
        }
    }
}
