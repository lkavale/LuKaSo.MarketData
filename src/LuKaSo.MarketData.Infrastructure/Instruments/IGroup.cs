using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.Instruments
{
    public interface IGroup : IEquatable<IGroup>
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Symbols
        /// </summary>
        IList<ISymbol> Symbols { get; set; }
    }
}
