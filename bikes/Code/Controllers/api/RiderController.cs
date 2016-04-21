using Bikes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bikes.Api
{
    public class RiderController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/rider")]
        public IEnumerable<Rider> Get()
        {
            return Rider.getRiders();
        }

        [HttpGet]
        [Route("api/rider/{id:int}")]
        public Rider Get(int id)
        {
            return Rider.getRider(id);
        }

        [HttpPost]
        [Route("api/rider")]
        public Rider Post(Rider rider)
        {
            rider.save();
            return rider;
        }

        [HttpPost]
        [Route("api/rider/{id:int}")]
        public Rider Post(int id, Rider rider)
        {
            rider.save();
            return rider;
        }

        [HttpDelete]
        [Route("api/rider/{id:int}")]
        public int Delete(int id)
        {
            Rider.deleteRider(id);
            return id;
        }

    }
}
