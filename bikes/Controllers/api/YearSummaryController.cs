using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Bikes.Model;
using System.Globalization;
using System.Drawing;

namespace Bikes.App
{
    public class YearSummaryController : ApiController
    {
        [HttpGet]
        public JObject Get()
        {
            int[] months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            //get all archived rides for this year
            IEnumerable<Ride> rides = Ride.getRides().Where(r => r.ride_date.Year == DateTime.Now.Year);
            //group rides by rider
            IEnumerable<IGrouping<int, Ride>> groups = rides.GroupBy(r => r.rider_id);
            //get the riders for reference lookups
            IEnumerable<Rider> riders = Rider.getRiders();

            //get the yearly summary imfo for each user
            JArray riderSummary =
                new JArray(groups.Select(g =>
                    new JObject(
                        new JProperty("rider", riderName(riders, g.Key)),
                        new JProperty("monthDistance", g.Where(r => r.ride_date.Month == DateTime.Now.Month).Sum(r => r.distance)),
                        new JProperty("yearDistance", g.Sum(r => r.distance)),
                        new JProperty("cash", g.Sum(r => r.reward).ToString("C"))))); 

            JObject yearRides = new JObject(
                new JProperty("labels",
                    //add a list of month names
                    new JArray(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames.Take(12)) ),
                new JProperty("datasets",
                    //create a dataset for each rider
                    new JArray(groups.Select(g => 
                        new JObject(
                            new JProperty("label", riderName(riders, g.Key)),
                            new JProperty("fillColor", riderColor(riders, g.Key, 112)),
                            new JProperty("strokeColor", riderColor(riders, g.Key, 200)),
                            new JProperty("highlightFill", riderColor(riders, g.Key, 180)),
                            new JProperty("highlightStroke", riderColor(riders, g.Key, 255)),
                            new JProperty("data",
                                //group the rides for each rider by month
                                new JArray(months.Select(i => 
                                    g.Where(r => r.ride_date.Month == i)
                                    .Sum(r => r.distance) ))))))));

            JObject result = new JObject(
                    new JProperty("chartData", yearRides),
                    new JProperty("riderSummary", riderSummary));

            BikesDebug.dumpToFile("yearRides.json", result.ToString(Newtonsoft.Json.Formatting.Indented));

            return result;

        }

        private String riderColor(IEnumerable<Rider> riders, int id, byte alpha)
        {
            Rider rider = riders.Where(r => r.id == id).First();

            Color color = Color.FromArgb(alpha, rider.color);

            return String.Format("rgba({0},{1},{2},{3:n2})", color.R, color.G, color.B, color.A/255f);
        }
        private String riderName(IEnumerable<Rider> riders, int id)
        {
            return riders.Where(r => r.id == id).First().name;
        }
    }
}
