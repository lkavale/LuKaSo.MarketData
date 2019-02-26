using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed;
using LuKaSo.MarketData.Infrastructure.Instruments;
using LuKaSo.MarketData.Types.Instruments;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Instruments
{
    public class DucascopySymbol : ISymbol
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Digits { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<IGroup> Groups { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Equals(ISymbol other)
        {
            throw new NotImplementedException();
        }
    }
}
