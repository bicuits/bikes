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
            IEnumerable<RideInfo> rides = RideInfo.getArchiveRides();
            IEnumerable<Rider> riders = Rider.getRiders();
            IEnumerable<IGrouping<int, RideInfo>> groups = rides.GroupBy(r => r.rider_id);

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
                                new JArray(g.GroupBy(mg => mg.ride_date.Month)
                                //sum the distances for each month
                                .Select(mg => mg.Sum(r => r.distance) ))))))));

            BikesDebug.dumpToFile("yearRides.json", yearRides.ToString(Newtonsoft.Json.Formatting.Indented));

            return yearRides;
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
