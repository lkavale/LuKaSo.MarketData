using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.Instruments
{
    public interface IInstrumentManager<T, U>
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

        /// <summary>
        /// Gets top level groups
        /// </summary>
        IEnumerable<U> TopLevelGroups { get; }

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        bool IsSymbolExists(T symbol);

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        bool IsSymbolExists(string symbolName);

        /// <summary>
        /// Get symbol by name
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        T GetSymbolByName(string symbolName);

        /// <summary>
        /// Contains group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        bool IsGroupExists(U group);

        /// <summary>
        /// Contains group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        bool IsGroupExists(string groupName);

        /// <summary>
        /// Get group by name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        U GetGroupByName(string groupName);
    }
}
