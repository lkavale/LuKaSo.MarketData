﻿using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.Instruments
{
    public interface ISymbol : IEquatable<ISymbol>
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
        /// Base currency
        /// </summary>
        string BaseCurrency { get; set; }

        /// <summary>
        /// Quota currency
        /// </summary>
        string QuotaCurrency { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        IList<IGroup> Groups { get; set; }
    }
}
