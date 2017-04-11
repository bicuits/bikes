using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Bikes.Model;

namespace Bikes.Api
{
    public class ReportController : BikesApiControllerBase
    {
        IDictionary<int, string> routes;
        IDictionary<int, string> bikes;
        IDictionary<int, string> riders;
        IEnumerable<Ride> rides;

        [HttpGet]
        [Route("api/report")]
        public JObject Get()
        {
            DateTime now = DateTime.Now;
            bikes = Bike.getBikes().ToDictionary(rt => rt.id, rt => rt.name);
            routes = Route.getRoutes().ToDictionary(rt => rt.id, rt => rt.name);
            riders = Rider.getRiders().ToDictionary(rt => rt.id, rt => rt.name);

            rides = Ride.getRides().Where(r => (r.ride_date - now).Days < 60 && r.ride_date.Year == now.Year);

            JObject result = new JObject(
                new JProperty("reports",
                    new JArray(
                        new JObject(
                            new JProperty("title", "Recent routes"),
                            new JProperty("data", getRouteSummary(rides))
                            ),
                        new JObject(
                            new JProperty("title", "Recent bikes"),
                            new JProperty("data", getBikeSummary(rides))
                            ),
                        new JObject(
                            new JProperty("title", "Recent rides"),
                            new JProperty("data", getRiderSummary(rides))
                            )

                            )));

            return result;
        }

        private JObject getRouteSummary(IEnumerable<Ride> rides)
        {
            IEnumerable<IGrouping<int, Ride>> rideGroupings = rides
                .GroupBy(r => r.route_id)
                .OrderByDescending(rg => rg.Count());

            JObject result = new JObject(
                new JProperty("headings",
                    new JArray(
                        new JObject(
                            new JProperty("caption", "Route")),
                        new JObject(
                            new JProperty("caption", "rides")),
                        new JObject(
                            new JProperty("caption", "distance")))),
                new JProperty("rows",
                    new JArray(rideGroupings.Select(rg =>
                        new JObject(
                            new JProperty("cells",
                                new JArray(
                                    new JObject(
                                        new JProperty("value", routes.valueOrDefault(rg.Key, "deleted"))),
                                    new JObject(
                                        new JProperty("value", rg.Count())),
                                    new JObject(
                                        new JProperty("value", (int)rg.Sum(r => r.distance))
                                    ))))))));

            return result;
        }

        private JObject getBikeSummary(IEnumerable<Ride> rides)
        {
            IEnumerable<IGrouping<int, Ride>> rideGroupings = rides
                .GroupBy(r => r.bike_id)
                .OrderByDescending(rg => rg.Count());

            JObject result = new JObject(
                new JProperty("headings",
                    new JArray(
                        new JObject(
                            new JProperty("caption", "Bike")),
                        new JObject(
                            new JProperty("caption", "rides")),
                        new JObject(
                            new JProperty("caption", "distance")))),
                new JProperty("rows",
                    new JArray(rideGroupings.Select(rg =>
                        new JObject(
                            new JProperty("cells",
                                new JArray(
                                    new JObject(
                                        new JProperty("value", bikes.valueOrDefault(rg.Key, "deleted"))),
                                    new JObject(
                                        new JProperty("value", rg.Count())),
                                    new JObject(
                                        new JProperty("value", (int)rg.Sum(r => r.distance))
                                    ))))))));

            return result;
        }

        private JObject getRiderSummary(IEnumerable<Ride> rides)
        {
            IEnumerable<IGrouping<int, Ride>> rideGroupings = rides
                .GroupBy(r => r.rider_id)
                .OrderByDescending(rg => rg.Count());

            JObject result = new JObject(
                new JProperty("headings",
                    new JArray(
                        new JObject(
                            new JProperty("caption", "Rider")),
                        new JObject(
                            new JProperty("caption", "rides")),
                        new JObject(
                            new JProperty("caption", "distance")))),
                new JProperty("rows",
                    new JArray(rideGroupings.Select(rg =>
                        new JObject(
                            new JProperty("cells",
                                new JArray(
                                    new JObject(
                                        new JProperty("value", riders.valueOrDefault(rg.Key, "deleted"))),
                                    new JObject(
                                        new JProperty("value", rg.Count())),
                                    new JObject(
                                        new JProperty("value", (int)rg.Sum(r => r.distance))
                                    ))))))));

            return result;
        }
    }
}
