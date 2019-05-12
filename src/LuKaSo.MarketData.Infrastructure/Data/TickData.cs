using System;

namespace LuKaSo.MarketData.Infrastructure.Data
{
    public class TickData : TimeData, IEquatable<TickData>
    {
        /// <summary>
        /// Ask price
        /// </summary>
        public decimal Ask { get; set; }

        /// <summary>
        /// Bid price
        /// </summary>
        public decimal Bid { get; set; }

        /// <summary>
        /// Equality check
        /// </summary>
        /// <param name="other">Object for comparison</param>
        /// <returns>Equality check result</returns>
        public bool Equals(TickData other)
        {
            return Time == other.Time
                && Ask.Equals(other.Ask)
                && Bid.Equals(other.Bid);
        }
    }
}
