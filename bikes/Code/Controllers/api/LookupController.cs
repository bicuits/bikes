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
        [Route("api/rawdata/{year:int}")]
        public Object Get (int year)
        {
            return new {
                
                bikes = Bike.getBikes(),
                riders = Rider.getRiders(),
                routes = Route.getRoutes(),
                rides = Ride.getRides().Select(r => new RideVM(r))
            };
        }
    }
}
