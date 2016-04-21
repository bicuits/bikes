using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bikes.Model;

namespace Bikes.Api
{
    public class BikeApiController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/bike")]
        public IEnumerable<Bike> Get()
        {
            return Bike.getBikes();
        }

        [HttpGet]
        [Route("api/bike/{id:int}")]
        public Bike Get(int id)
        {
            return Bike.getBike(id);
        }

        [HttpPost]
        [Route("api/bike")]
        public Bike Post(Bike bike)
        {
            bike.save();
            return bike;
        }

        [HttpPost]
        [Route("api/bike/{id:int}")]
        public Bike Post(int id, Bike bike)
        {
            bike.save();
            return bike;
        }

        [HttpDelete]
        [Route("api/bike/{id:int}")]
        public int Delete(int id)
        {
            Bike.deleteBike(id);
            return id;
        }
    }
}
