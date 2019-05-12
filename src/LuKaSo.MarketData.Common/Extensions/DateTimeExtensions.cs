using System;
using System.Globalization;

namespace LuKaSo.MarketData.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Floor date time to entire hours
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FloorToHours(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, dateTime.Kind);
        }

        /// <summary>
        /// Ceil date time to entire hours
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime CeilToHours(this DateTime dateTime)
        {
            var floorTime = dateTime.FloorToHours();

            if (dateTime > floorTime)
            {
                return floorTime.AddHours(1);
            }

            return floorTime;
        }

        /// <summary>
        /// Floor date time to entire days
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FloorToDays(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind);
        }

        /// <summary>
        /// Ceil date time to entire days
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime CeilToDays(this DateTime dateTime)
        {
            var floorTime = dateTime.FloorToDays();

            if (dateTime > floorTime)
            {
                return floorTime.AddDays(1);
            }

            return floorTime;
        }

        /// <summary>
        /// Stringify DateTime?
        /// </summary>
        /// <param name="dateTime">Date time</param>
        /// <returns></returns>
        public static string ToString(this DateTime? dateTime)
        {
            if (dateTime != null)
            {
                return ((DateTime)dateTime).ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Stringify DateTime?
        /// </summary>
        /// <param name="dateTime">Date time</param>
        /// <param name="ci">Culture info</param>
        /// <returns></returns>
        public static string ToString(this DateTime? dateTime, CultureInfo ci)
        {
            if (dateTime != null)
            {
                return ((DateTime)dateTime).ToString(ci);
            }

            return string.Empty;
        }
    }
}
