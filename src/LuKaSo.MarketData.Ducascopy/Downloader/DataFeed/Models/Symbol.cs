using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models
{
    public class Symbol
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Symbol()
        {
            TagList = new List<string>();
        }

        /// <summary>
        /// Get start date depend on desired data resolution
        /// </summary>
        /// <param name="dataResolution">Data resolution</param>
        /// <returns></returns>
        public DateTime GetStartDateByResolution(DataResolution dataResolution)
        {
            switch (dataResolution)
            {
                case DataResolution.TickData:
                    return StartTick;
                case DataResolution.Second10:
                    return Start10Sec;
                case DataResolution.Minute:
                    return Start60Sec;
                case DataResolution.Hour:
                    return Start60Min;
                case DataResolution.Day:
                default:
                    return StartDay;
            }
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
        /// Title, "title": "AAL.GB/GBX",
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Name, "name": "AAL.GB/GBP",
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Name, "name": "AAL.GB/GBP",
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Name for data feed, "historical_filename": "AALGBGBX",
        /// </summary>
        [JsonProperty("historical_filename")]
        public string DataFeedName { get; set; }

        /// <summary>
        /// Pip value, "pipValue": 0.01,
        /// </summary>
        [JsonProperty("pipValue")]
        public double PipValue { get; set; }

        /// <summary>
        /// Base currency, "base_currency": "AAL.GB",
        /// </summary>
        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Quote currency, "quote_currency": "GBP",
        /// </summary>
        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }

        /// <summary>
        /// Comodities per constract, "commodities_per_contract": 100,
        /// </summary>
        [JsonProperty("commodities_per_contract")]
        public double? ComoditiesPercontract { get; set; }

        /// <summary>
        /// Tag list, 
        /// 
        /// "tag_list": [
        ///     "CFD_INSTRUMENTS"
        /// ],
        /// </summary>
        [JsonProperty("tag_list")]
        public IList<string> TagList { get; set; }

        /// <summary>
        /// Start of tick data, "history_start_tick": 1467183584010,
        /// </summary>
        [JsonProperty("history_start_tick")]
        [JsonConverter(typeof(JsonUnixTimeConvertor))]
        public DateTime StartTick { get; set; }

        /// <summary>
        /// Start of 10s data, "history_start_10sec": 1467273600000,
        /// </summary>
        [JsonProperty("history_start_10sec")]
        [JsonConverter(typeof(JsonUnixTimeConvertor))]
        public DateTime Start10Sec { get; set; }

        /// <summary>
        /// Start of 60s data, "history_start_60sec": 1467273600000,
        /// </summary>
        [JsonProperty("history_start_60sec")]
        [JsonConverter(typeof(JsonUnixTimeConvertor))]
        public DateTime Start60Sec { get; set; }

        /// <summary>
        /// Start of 60min data, "history_start_60min": 1294128000000,
        /// </summary>
        [JsonProperty("history_start_60min")]
        [JsonConverter(typeof(JsonUnixTimeConvertor))]
        public DateTime Start60Min { get; set; }

        /// <summary>
        /// Start of day data, "history_start_day": 1294099200000
        /// </summary>
        [JsonProperty("history_start_day")]
        [JsonConverter(typeof(JsonUnixTimeConvertor))]
        public DateTime StartDay { get; set; }
    }
}
