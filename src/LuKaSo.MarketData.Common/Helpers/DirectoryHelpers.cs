using System.IO;

namespace LuKaSo.MarketData.Common.Helpers
{
    public static class DirectoryHelpers
    {
        /// <summary>
        /// Create directory if not exists
        /// </summary>
        /// <param name="fileName">File name</param>
        public static void DirectoryCreateIfNotExist(string fileName)
        {
            var path = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
