using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Infrastructure.Reader
{
    public interface ITickDataReader<T>: IDisposable
    {
        T Read();
        bool CanProgressReport { get; }
        bool IsEndOfStream { get; }
    }
}
