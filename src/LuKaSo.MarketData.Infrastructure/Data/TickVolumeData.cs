using System;

namespace LuKaSo.MarketData.Infrastructure.Data
{
    public class TickVolumeData : TickData, IEquatable<TickVolumeData>
    {
        public static implicit operator TickVolumeData(TickAskBidVolumeData data)
        {
            return new TickVolumeData()
            {
                Time = data.Time,
                Ask = data.Ask,
                Bid = data.Bid,
                Volume = data.AskVolume + data.BidVolume
            };
        }

        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Equality check
        /// </summary>
        /// <param name="other">Object for comparison</param>
        /// <returns>Equality check result</returns>
        public bool Equals(TickVolumeData other)
        {
            return Time == other.Time
                && Ask.Equals(other.Ask)
                && Bid.Equals(other.Bid)
                && Volume.Equals(other.Volume);
        }
    }
}
