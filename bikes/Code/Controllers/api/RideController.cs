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
        public IEnumerable<RideVM> Get()
        {
            return Ride.getRides()
                .OrderByDescending(r => r.ride_date)
                .Select(r => new RideVM(r));
        }

        [HttpGet]
        [Route("api/ride/{id:int}")]
        public RideVM Get(int id)
        {
            return new RideVM(Ride.getRide(id));
        }

        [HttpPost]
        [Route("api/ride")]
        public RideVM Post(RideVM vm)
        {
            //calculate the reward and distance
            double rideLength;
            decimal reward;
            Rider rider;

            if (vm.route_id == Route.DefaultId)
            {
                rideLength = vm.distance;
            }
            else
            {
                rideLength = Route.getRoute(vm.route_id).distance;
                if (vm.returnRide)
                {
                    rideLength *= 2;
                }
            }

            rider = Rider.getRider(vm.rider_id);

            reward = vm.payable ? vm.bonus + (decimal)((rideLength * rider.rate) / 100) : 0m;

            //create the ride
            Ride ride = Ride.add(
                vm.bike_id, 
                vm.rider_id, 
                vm.route_id,
                DateTime.Parse(vm.ride_date),
                vm.notes,
                Math.Round(reward, 2),
                Math.Round(rideLength, 1));

            //return the new ride as a view model
            return new RideVM(ride);
        }

        [HttpPost]
        [Route("api/ride/{id:int}")]
        public RideVM Post(int id, RideVM vm)
        {
            //update the notes.  This is the only field that is not immutable
            Ride.setNotes(id, vm.notes);

            //return the ride info
            return new RideVM(Ride.getRide(id));
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
