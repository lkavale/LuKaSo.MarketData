using LuKaSo.MarketData.Infrastructure.Instruments;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Instruments
{
    public class DucascopySymbol : ISymbol
    {
        public DucascopySymbol()
        {
            //Groups = new List<DucascopyGroup>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DirectoryName { get; set; }
        public int Digits { get; set; }
        public IList<IGroup> Groups { get; set; }

        public bool Equals(ISymbol other)
        {
            return Name == other.Name;
        }
    }
}
