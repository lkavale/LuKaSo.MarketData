using LuKaSo.MarketData.Infrastructure.Instruments;
using LuKaSo.MarketData.Types.Instruments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Instruments
{
    public class DucascopyGroup : IGroup
    {
        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string Description { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IList<ISymbol> Symbols { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public bool Equals(IGroup other)
        {
            throw new System.NotImplementedException();
        }
    }
}
