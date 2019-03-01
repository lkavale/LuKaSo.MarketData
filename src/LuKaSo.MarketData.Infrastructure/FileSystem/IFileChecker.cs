using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Infrastructure.FileSystem
{
    public interface IFileChecker<T>
        where T : ISymbol
    {
        IEnumerable<IFile> GetMissingFiles(T symbol, DateTime start, DateTime end);
        IEnumerable<DateTime> GetMissingDates(T symbol, DateTime start, DateTime end);
    }
}
