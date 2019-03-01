using LuKaSo.MarketData.Infrastructure.Instruments;
using System;

namespace LuKaSo.MarketData.Infrastructure.FileSystem
{
    public interface IFileNameGenerator<T>
        where T : ISymbol
    {
        string GenerateFileName(T symbol, DateTime dateTime);
    }
}
