using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Infrastructure.Instruments;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Common.Instruments
{
    public class InstrumentManager<T, U> : IInstrumentManager<T, U>
        where T : ISymbol
        where U : IGroup
    {
        /// <summary>
        /// Datafeed configuration
        /// </summary>
        private readonly IDataFeedConfiguration<T, U> _dataFeedConfiguration;

        /// <summary>
        /// Instrument manager
        /// </summary>
        /// <param name="configurationReader"></param>
        public InstrumentManager(IDataFeedConfiguration<T, U> dataFeedConfiguration)
        {
            _dataFeedConfiguration = dataFeedConfiguration;
        }

        /// <summary>
        /// Gets all symbols
        /// </summary>
        public IEnumerable<T> Symbols
        {
            get
            {
                return _dataFeedConfiguration.Symbols;
            }
        }

        /// <summary>
        /// Gets all groups
        /// </summary>
        public IEnumerable<U> Groups
        {
            get
            {
                return _dataFeedConfiguration.Groups;
            }
        }

        /// <summary>
        /// Gets top level groups
        /// </summary>
        public IEnumerable<U> TopLevelGroups
        {
            get
            {
                return _dataFeedConfiguration.Groups.Where(g => g.Parent == null);
            }
        }

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public bool IsSymbolExists(T symbol)
        {
            return _dataFeedConfiguration.Symbols.Contains(symbol);
        }

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public bool IsSymbolExists(string symbolName)
        {
            return _dataFeedConfiguration.Symbols.Any(s => s.Name == symbolName);
        }

        /// <summary>
        /// Get symbol by name
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public T GetSymbolByName(string symbolName)
        {
            return _dataFeedConfiguration.Symbols.Single(s => s.Name == symbolName);
        }

        /// <summary>
        /// Contains group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool IsGroupExists(U group)
        {
            return _dataFeedConfiguration.Groups.Contains(group);
        }

        /// <summary>
        /// Contains group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool IsGroupExists(string groupName)
        {
            return _dataFeedConfiguration.Groups.Any(s => s.Name == groupName);
        }

        /// <summary>
        /// Get group by name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public U GetGroupByName(string groupName)
        {
            return _dataFeedConfiguration.Groups.Single(s => s.Name == groupName);
        }
    }
}
