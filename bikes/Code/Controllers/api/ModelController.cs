using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Drawing;

using Bikes.Model;
using Bikes.Model.Banking;
using System.Globalization;

namespace Bikes.Api
{
    public class ModelController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/model/{year:int}")]
        public JObject Get(int year)
        {
            //get all archived rides for the requested year
            IEnumerable<Ride> rides = Ride.getRides().Where(r => r.ride_date.Year == year);

            //TO DO: get the month from the query string
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();

            JObject result = new JObject(
                new JProperty("bikes",
                    new JArray(Bike.getBikes().OrderBy(b => b.name).Select(b => JObject.FromObject(b)))),

                new JProperty("riders",
                    new JArray(Rider.getRiders().OrderBy(r => r.name).Select(r => JObject.FromObject(r)))),

                new JProperty("routes",
                    new JArray(Route.getRoutes().OrderBy(r => r.name).Select(r => JObject.FromObject(r)))),

                new JProperty("rides",
                    new JArray(rides.OrderByDescending(r => r.ride_date).Select(r => JObject.FromObject(new RideVM(r))))),

                new JProperty("payments",
                    new JArray(Payment.getPayments().OrderBy(p => p.paid_date).Select(p => p.toJObject()))),

                new JProperty("colorList", getChartColors()),

                new JProperty("months", Enumerable.Range(1, 12).Select(i => new JObject(
                    new JProperty("month", i), new JProperty("caption", dtfi.GetAbbreviatedMonthName(i))))),

                new JProperty("riderSummary", getRiderSummary(rides))
            );

            App.BikesDebug.dumpToFile("model.json", result.ToString(Newtonsoft.Json.Formatting.Indented));

            return result;
        }

        private JArray getChartColors()
        {
            return new JArray(
                Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Where(k => k >= KnownColor.Transparent && k < KnownColor.ButtonFace) //Exclude system colors
                .Select(k => Color.FromKnownColor(k))
                .OrderBy(c => c.GetHue())
                .ThenBy(c => c.GetSaturation())
                .ThenBy(c => c.GetBrightness())
                .Select(c => ColorTranslator.ToHtml(c)));
        }

        private JArray getRiderSummary(IEnumerable<Ride> rides)
        {
            //group rides by rider
            IEnumerable<IGrouping<int, Ride>> groups = rides.GroupBy(r => r.rider_id);

            //get the riders for reference lookups
            IEnumerable<Rider> riders = Rider.getRiders();

            //get the yearly summary imfo for each user
            JArray riderSummary =
                new JArray(groups.Select(g =>
                    new JObject(
                        new JProperty("riderId", g.Key),
                        new JProperty("rider", g.riderName(riders)),
                        new JProperty("monthDistance", Math.Round(
                            g.Where(r => r.ride_date.Month == DateTime.Now.Month).Sum(r => r.distance), 1)),
                        new JProperty("yearDistance", Math.Round(
                            g.Sum(r => r.distance))),
                        new JProperty("cashYear", g.Sum(r => r.reward).ToString("C")),
                        new JProperty("cashUnpaid", g.Where(r => r.paid == false).Sum(r => r.reward).ToString("C")))));


            return riderSummary;

        }
    }
}

