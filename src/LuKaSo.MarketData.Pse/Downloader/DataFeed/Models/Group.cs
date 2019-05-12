using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Pse.Downloader.DataFeed.Models
{
    public class Group
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Group()
        {
            Instruments = new List<string>();
        }

        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Parent
        /// </summary>
        [JsonProperty("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Instrumnets
        /// </summary>
        [JsonProperty("instruments")]
        public List<string> Instruments { get; set; }
    }
}
