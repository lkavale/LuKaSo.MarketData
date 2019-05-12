using System;

namespace LuKaSo.MarketData.Common.Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// Append relative path to base address
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static Uri Append(this Uri basePath, string relativePath)
        {
            relativePath = relativePath.TrimStart(new[] { '/', '\\' });
            var basePathString = basePath.ToString().TrimEnd(new[] { '/', '\\' });

            return new Uri(basePathString + '/' + relativePath);
        }
    }
}
