using LuKaSo.MarketData.Infrastructure.Instruments;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Infrastructure
{
    public interface IDataFeedConfiguration<T, U>
        where T : ISymbol
        where U : IGroup
    {
        /// <summary>
        /// Gets all symbols
        /// </summary>
        IEnumerable<T> Symbols { get; }

        /// <summary>
        /// Gets all groups
        /// </summary>
        IEnumerable<U> Groups { get; }
    }
}
