using System.IO;

namespace LuKaSo.MarketData.Common.Helpers
{
    public static class DirectoryHelpers
    {
        /// <summary>
        /// Create directory if not exists
        /// </summary>
        /// <param name="directoryName">Directory name</param>
        public static void DirectoryCreateIfNotExist(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}
