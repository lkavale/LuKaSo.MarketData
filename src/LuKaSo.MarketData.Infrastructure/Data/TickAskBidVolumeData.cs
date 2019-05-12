using System;

namespace LuKaSo.MarketData.Infrastructure.Data
{
    public class TickAskBidVolumeData : TickData, IEquatable<TickAskBidVolumeData>
    {
        /// <summary>
        /// Ask volume
        /// </summary>
        public decimal AskVolume { get; set; }

        /// <summary>
        /// Bid volume
        /// </summary>
        public decimal BidVolume { get; set; }

        /// <summary>
        /// Equality check
        /// </summary>
        /// <param name="other">Object for comparison</param>
        /// <returns>Equality check result</returns>
        public bool Equals(TickAskBidVolumeData other)
        {
            return Time == other.Time
                && Ask.Equals(other.Ask)
                && Bid.Equals(other.Bid)
                && AskVolume.Equals(other.AskVolume)
                && BidVolume.Equals(other.BidVolume);
        }
    }
}
