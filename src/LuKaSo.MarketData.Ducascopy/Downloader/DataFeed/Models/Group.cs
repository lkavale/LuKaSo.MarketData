using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models
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
        /// Id, "id": "US",
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Title,  "title": "US",
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Parent, "parent": "STCK_CFD",
        /// </summary>
        [JsonProperty("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Instrumnets,       
        /// 
        /// "instruments": [
        ///     "A.US/USD",
        ///     ...
        /// </summary>
        [JsonProperty("instruments")]
        public List<string> Instruments { get; set; }
    }
}
