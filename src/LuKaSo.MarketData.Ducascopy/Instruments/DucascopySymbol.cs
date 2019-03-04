using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Instruments
{
    public class DucascopySymbol : ISymbol
    {
        public DucascopySymbol()
        {
            Groups = new List<IGroup>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Base currency
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Quota currency
        /// </summary>
        public string QuotaCurrency { get; set; }

        /// <summary>
        /// Directory name
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// Digits
        /// </summary>
        public int Digits { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        public DateTime StartDate { get; set; }

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