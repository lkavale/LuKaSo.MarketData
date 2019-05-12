using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Pse.Instruments
{
    public sealed class PseSymbol : ISymbol
    {
        public PseSymbol()
        {
            Groups = new List<IGroup>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ISIN
        /// </summary>
        public string Isin { get; set; }

        /// <summary>
        /// Notation id
        /// </summary>
        public string NotationId { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Pip value
        /// </summary>
        public double PipValue { get; set; }

        /// <summary>
        /// Base currency
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Quota currency
        /// </summary>
        public string QuotaCurrency { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        public IList<IGroup> Groups { get; set; }

        /// <summary>
        /// Equality comparison
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ISymbol other)
        {
            return Name == other.Name;
        }
    }
}