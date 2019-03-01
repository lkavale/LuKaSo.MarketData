using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.FileSystem
{
    public interface IFileManager<T>
        where T : ISymbol
    {
        DateTime GetStartDateTime(T symbol);
        DateTime GetEndDateTime(T symbol);
        IEnumerable<DateTime> GetDateTimes(T symbol);
    }
}
