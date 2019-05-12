using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Pse.Downloader.DataFeed.Models
{
    public class Symbol
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Symbol()
        {
        }

        /// <summary>
        /// Get digits
        /// </summary>
        /// <returns></returns>
        public int GetDigits()
        {
            return (int)Math.Abs(Math.Log10(PipValue));
        }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// ISIN
        /// </summary>
        [JsonProperty("isin")]
        public string Isin { get; set; }

        /// <summary>
        /// Notation id
        /// </summary>
        [JsonProperty("notation_id")]
        public string NotationId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Pip value
        /// </summary>
        [JsonProperty("pipValue")]
        public double PipValue { get; set; }

        /// <summary>
        /// Base currency
        /// </summary>
        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Quote currency
        /// </summary>
        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }

        /// <summary>
        /// Tag list
        /// </summary>
        [JsonProperty("tag_list")]
        public IList<string> TagList { get; set; }
    }
}
