using Newtonsoft.Json;
using System;

namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed
{
    /// <summary>
    /// Unix time Json convertor
    /// </summary>
    public class JsonUnixTimeConvertor : JsonConverter
    {
        /// <summary>
        /// Can convert for datetime objects
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        /// <summary>
        /// Read JSON
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var ms = long.Parse(reader.Value.ToString());
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ms);
        }

        /// <summary>
        /// Write JSON
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!CanConvert(value.GetType()))
            {
                throw new ArgumentException($"Object {value.GetType()} is not DateTime.");
            }

            var dt = (DateTime)value;
            var ms = (long)dt.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            if (ms < 0)
            {
                throw new ArgumentOutOfRangeException($"Date {dt.ToString()} is before unix epoch.");
            }

            writer.WriteValue(ms);
        }
    }
}
