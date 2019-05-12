using LuKaSo.MarketData.Infrastructure.Downloader;
using System;

namespace LuKaSo.MarketData.Types.Downloader
{
    public class Configuration : IConfiguration
    {
        /// <summary>
        /// Default data path
        /// </summary>
        private const string _defaultDataPath = "C:/Data/Ducascopy/";

        private string _dataPath;

        public string DataPath
        {
            get
            {
                if (string.IsNullOrEmpty(_dataPath))
                {
                    return _defaultDataPath;
                }

                return _dataPath;
            }
            set
            {
                _dataPath = value;
            }
        }
    }
}
