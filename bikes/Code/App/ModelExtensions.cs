using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bikes.Model;
using System.Text.RegularExpressions;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace Bikes.Api
{
    public static class ModelExtensions
    {
        //public static RideVM toRideVM(this Ride ride)
        //{
        //    return new RideVM(ride);
        //}
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

        public static string chartColor(this IGrouping<int, Ride> group, IEnumerable<Rider> riders, byte alpha)
        {
            Rider rider = riders.Where(r => r.id == group.Key).First();
            Color color = ColorTranslator.FromHtml(rider.color_code);

            return String.Format("rgba({0},{1},{2},{3:n2})", color.R, color.G, color.B, color.A / 255f);
        }

        public static JObject toJObject(this Bike bike)
        {
            return new JObject(
                new JProperty("id", bike.id.ToString()),
                new JProperty("name", bike.name));
        }

        public static JObject toJObject(this Rider rider)
        {
            return new JObject(
                new JProperty("id", rider.id.ToString()),
                new JProperty("name", rider.name),
                new JProperty("bank_branch_id", rider.bank_branch_id),
                new JProperty("bank_customer_id", rider.bank_customer_id),
                new JProperty("bank_account_id", rider.bank_account_id));
        }

        public static JObject toJObject(this Route route)
        {
            return new JObject(
                new JProperty("id", route.id.ToString()),
                new JProperty("name", route.name),
                new JProperty("distance", route.distance.ToString()),
                new JProperty("notes", route.notes));
        }
    }
}