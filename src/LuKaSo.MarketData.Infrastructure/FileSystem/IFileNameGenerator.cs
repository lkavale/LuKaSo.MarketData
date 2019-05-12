using LuKaSo.MarketData.Infrastructure.Instruments;
using System;

namespace LuKaSo.MarketData.Infrastructure.FileSystem
{
    public interface IFileNameGenerator<T>
        where T : ISymbol
    {
        /// <summary>
        /// Generate source file path
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        string GenerateSourceFileName(T symbol, DateTime dateTime);

        /// <summary>
        /// Generate destination file path
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        string GenerateDestinationFileName(T symbol, DateTime dateTime);

        /// <summary>
        /// Generate file
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        IFile GenerateFile(T symbol, DateTime dateTime);
    }
}
