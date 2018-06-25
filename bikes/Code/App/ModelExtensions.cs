using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing;
using Newtonsoft.Json.Linq;

using Bikes.Model;
using Bikes.Model.Banking;
using System.Globalization;

namespace Bikes.Api
{
    public static class ModelExtensions
    {
        public static String notesSummary(this Ride ride)
        {
            return getSummary(ride.notes);
        }

        private static String getSummary(String src)
        {
            String result;
            const int maxLen = 30;

            if (src == null)
            {
                result = "";
            }
            else if (src.Length < maxLen)
            {
                result = src;
            }
            else
            {
                result = src.Substring(0, maxLen - 1) + "...";
            }
            return result;

        }

        public static int? ToNullableBankId(this string idStr, int defaultValue)
        {
            int? result = null;

            if (idStr != null)
            {
                //strip out any additions from anglular, such as "string:2" or "number:3"
                //idStr = Regex.Replace(idStr, "[^0-9]", "");

                int i;
                if (int.TryParse(idStr, out i) && i != defaultValue)
                {
                    result = i;
                }
            }

            return result;
        }

        public static string riderName(this IGrouping<int, Ride> group, IEnumerable<Rider> riders)
        {
            return riders.Where(r => r.id == group.Key).First().name;
        }

        public static string routeName(this IGrouping<int, Ride> group, IEnumerable<Route> routes)
        {
            return routes.Where(r => r.id == group.Key).First().name;
        }

        public static string chartColor(this IGrouping<int, Ride> group, IEnumerable<Rider> riders, byte alpha)
        {
            Rider rider = riders.Where(r => r.id == group.Key).First();
            Color color = ColorTranslator.FromHtml(rider.color_code);

            return String.Format("rgba({0},{1},{2},{3:n2})", color.R, color.G, color.B, color.A / 255f);
        }

        public static JObject toJObject(this Payment p)
        {
            return new JObject(
                new JProperty("rider", p.rider),
                new JProperty("amount", p.amount.ToString("C", new CultureInfo("en-GB"))),
                new JProperty("paid_date",
                    p.paid_date.HasValue ? p.paid_date.Value.ToString("dd/MM/yyyy") : ""));
        }

        public static TValue valueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue result;

            if (!dictionary.TryGetValue(key, out result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}