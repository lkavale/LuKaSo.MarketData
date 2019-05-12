using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Pse.Downloader.DataFeed.Models
{
    public class Configuration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Configuration()
        {
            Symbols = new Dictionary<string, Symbol>();
            Groups = new Dictionary<string, Group>();
        }

        /// <summary>
        /// Instruments
        /// </summary>
        [JsonProperty("instruments")]
        public IDictionary<string, Symbol> Symbols { get; set; }

        /// <summary>
        /// Groups of instruments
        /// </summary>
        [JsonProperty("groups")]
        public IDictionary<string, Group> Groups { get; set; }
    }
}
