using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace LuKaSo.MarketData.Common.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static string ToQueryString(this NameValueCollection collection)
        {
            return "?" + collection.AllKeys
                 .Select(k =>
                 {
                     var key = HttpUtility.UrlEncode(k);
                     var value = HttpUtility.UrlEncode(string.Join(",", collection.GetValues(k)));
                     return $"{key}={value}";
                 })
                 .Aggregate((a, b) => $"{a}&{b}");
        }
    }
}
