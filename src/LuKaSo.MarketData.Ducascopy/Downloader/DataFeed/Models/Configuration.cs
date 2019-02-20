using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models
{
    public class Configuration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Configuration()
        {
            Tags = new List<Tag>();
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

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public ICollection<Tag> Tags { get; set; }
    }
}
