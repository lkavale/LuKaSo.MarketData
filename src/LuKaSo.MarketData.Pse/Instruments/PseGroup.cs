using LuKaSo.MarketData.Infrastructure.Instruments;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Pse.Instruments
{
    public sealed class PseGroup : IGroup
    {
        public PseGroup()
        {
            Symbols = new List<ISymbol>();
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
        /// Parent group
        /// </summary>
        public IGroup Parent { get; set; }

        /// <summary>
        /// Groups
        /// </summary>
        public IList<IGroup> Groups { get; set; }

        /// <summary>
        /// Symbols
        /// </summary>
        public IList<ISymbol> Symbols { get; set; }

        /// <summary>
        /// Equal comparison
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IGroup other)
        {
            return Name == other.Name;
        }
    }
}
