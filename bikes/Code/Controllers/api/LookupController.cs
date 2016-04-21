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
    public class LookupController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/lookup")]
        public JObject Get ()
        {
            List<Bike> bikes = Bike.getBikes();
            List<Rider> riders = Rider.getRiders();
            List<Route> routes = Route.getRoutes();

            return new JObject(
                new JProperty("bikes", 
                    new JArray(bikes.Select(b => b.toJObject()))),
                new JProperty("riders",
                    new JArray(riders.Select(r => r.toJObject()))),
                new JProperty("routes",
                    new JArray(routes.Select(r => r.toJObject())))
                    );
        }
    }
}
