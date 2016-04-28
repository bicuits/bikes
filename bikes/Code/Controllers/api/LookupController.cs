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
    public class LookupController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/model/{year:int}")]
        public JObject Get(int year)
        {
            //DO DO: egt the month from the query string

            JObject result = new JObject(
                new JProperty("bikes",
                    new JArray(Bike.getBikes().OrderBy(b => b.name).Select(b => JObject.FromObject(b)))),

                new JProperty("riders",
                    new JArray(Rider.getRiders().OrderBy(r => r.name).Select(r => JObject.FromObject(r)))),

                new JProperty("routes",
                    new JArray(Route.getRoutes().OrderBy(r => r.name).Select(r => JObject.FromObject(r)))),

                new JProperty("rides",
                    new JArray(Ride.getRides().OrderByDescending(r => r.ride_date).Select(r => JObject.FromObject(new RideVM(r))))),

                new JProperty("payments",
                    new JArray(Payment.getPayments().OrderBy(p => p.paid_date).Select(p => p.toJObject()))),

                new JProperty("chartColors", getChartColors()),

                new JProperty("yearSummary", getYearSummary(year)),

                new JProperty("monthSummary", getMonthSummary(DateTime.Today.Month))
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

        private JObject getYearSummary(int year)
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
                        new JProperty("rider", g.riderName(riders)),
                        new JProperty("monthDistance", Math.Round(
                            g.Where(r => r.ride_date.Month == DateTime.Now.Month).Sum(r => r.distance), 1)),
                        new JProperty("yearDistance", Math.Round(
                            g.Sum(r => r.distance))),
                        new JProperty("cashYear", g.Sum(r => r.reward).ToString("C")),
                        new JProperty("cashUnpaid", g.Where(r => r.paid == false).Sum(r => r.reward).ToString("C")))));

            JObject yearRides = new JObject(
                new JProperty("labels",
                    //add a list of month names
                    new JArray(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames.Take(12))),
                new JProperty("datasets",
                    //create a dataset for each rider
                    new JArray(groups.Select(g =>
                        new JObject(
                            new JProperty("label", g.riderName(riders)),
                            new JProperty("fillColor", g.chartColor(riders, 112)),
                            new JProperty("strokeColor", g.chartColor(riders, 200)),
                            new JProperty("highlightFill", g.chartColor(riders, 180)),
                            new JProperty("highlightStroke", g.chartColor(riders, 255)),
                            new JProperty("data",
                                //group the rides for each rider by month
                                new JArray(months.Select(i =>
                                    Math.Round(
                                        g.Where(r => r.ride_date.Month == i).Sum(r => r.distance), 2)
                                        ))))))));

            JObject result = new JObject(
                    new JProperty("chartData", yearRides),
                    new JProperty("riderSummary", riderSummary));

            //App.BikesDebug.dumpToFile("yearRides.json", result.ToString(Newtonsoft.Json.Formatting.Indented));

            return result;

        }

        private JObject getMonthSummary(int month)
        {
            //get the rides for this month
            IEnumerable<Ride> rides = Ride.getRides().Where(r => r.ride_date.Month == DateTime.Now.Month);
            IEnumerable<IGrouping<int, Ride>> groups = rides.GroupBy(r => r.rider_id);
            IEnumerable<Rider> riders = Rider.getRiders();

            JObject recentRides = new JObject(
                new JProperty("labels",
                    new JArray(DateTime.Now.ToString("MMMM"))),
                new JProperty("datasets",
                    new JArray(groups.Select(g =>
                        new JObject(
                            new JProperty("label", g.riderName(riders)),
                            new JProperty("fillColor", g.chartColor(riders, 112)),
                            new JProperty("strokeColor", g.chartColor(riders, 200)),
                            new JProperty("highlightFill", g.chartColor(riders, 180)),
                            new JProperty("highlightStroke", g.chartColor(riders, 255)),
                            new JProperty("data",
                                new JArray(Math.Round(
                                    g.Sum(r => r.distance), 2)
                                    )))))));
            return recentRides;
        }

    }
}

