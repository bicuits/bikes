using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Bikes.Model;

namespace Bikes.App
{
    public class RiderSummaryController : ApiController
    {
        public JObject Get()
        {
            IEnumerable<Ride> rides = Ride.getRides();
            IEnumerable<IGrouping<int, Ride>> groups = rides.GroupBy(r => r.rider_id);

            JObject recentRides = new JObject(
                new JProperty("labels", 
                    new JArray(groups.Select(g => g.First().rider))),
                new JProperty("datasets",
                    new JArray(
                        new JObject(
                            new JProperty("fillColor", "rgba(220, 220, 220, 0.5)"),
                            new JProperty("strokeColor", "rgba(220, 220, 220, 0.8)"),
                            new JProperty("highlightFill", "rgba(220, 220, 220, 0.75)"),
                            new JProperty("highlightStroke", "rgba(220, 220, 220, 1)"),
                            new JProperty("data",
                                new JArray(groups.Select(g => g.Sum(r => r.rideLength)
                                )))))));

            BikesDebug.dumpToFile("recentRides.json", recentRides.ToString(Newtonsoft.Json.Formatting.Indented));

            return recentRides;
        }
    }
}
