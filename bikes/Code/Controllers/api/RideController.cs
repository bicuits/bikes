using Bikes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bikes.Api
{
    public class RideController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/ride")]
        public IEnumerable<Ride> Get()
        {
            return Ride.getRides();
        }

        [HttpGet]
        [Route("api/ride/{id:int}")]
        public Ride Get(int id)
        {
            return Ride.getRide(id);
        }

        [HttpPost]
        [Route("api/ride")]
        public Ride Post(Ride ride)
        {
            Ride.save();
            return ride;
        }

        [HttpPost]
        [Route("api/ride/{id:int}")]
        public Ride Post(int id, Ride ride)
        {
            ride.save();
            return ride;
        }

        [HttpDelete]
        [Route("api/ride/{id:int}")]
        public int Delete(int id)
        {
            Ride.deleteRide(id);
            return id;
        }

    }
}
