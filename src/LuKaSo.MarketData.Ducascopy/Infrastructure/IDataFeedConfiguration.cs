using LuKaSo.MarketData.Infrastructure.Instruments;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Infrastructure
{
    public interface IDataFeedConfiguration
    {
        /// <summary>
        /// Gets all symbols
        /// </summary>
        IEnumerable<ISymbol> Symbols { get; }

        /// <summary>
        /// Gets all groups
        /// </summary>
        IEnumerable<IGroup> Groups { get; }

        /// <summary>
        /// Gets top level groups
        /// </summary>
        IEnumerable<IGroup> TopLevelGroups { get; }

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        bool IsSymbolExists(ISymbol symbol);

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        bool IsSymbolExists(string symbolName);
    }
}
