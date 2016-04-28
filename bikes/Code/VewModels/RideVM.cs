using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.Api
{
    //RideVM is a view model for the Ride class.
    //Ride objects are mostly immutable.  Only the notes field can be updated once a ride is created.
    public class RideVM
    {
        public int id { get; set; }
        public double distance { get; set; }
        public string rider { get; private set; }
        public string route { get; private set; }
        public string bike { get; private set; }
        public decimal reward { get; set; }
        public String ride_date { get; set; }
        public String notes { get; set; }

        //The following are transient fields, only used for creating rides
        public bool returnRide { get; set; }
        public bool payable { get; set; }
        public int rider_id { get; set; }
        public int route_id { get; set; }
        public int bike_id { get; set; }
        public decimal bonus { get; set; }

        public RideVM()
        {
            bike_id = 0;
            rider_id = 0;
            route_id = 0;

            bike = "other";
            rider = "other";
            route = "other";

            returnRide = false;
            payable = true;

            ride_date = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public RideVM(Ride ride)
            : this()
        {
            id = ride.id;

            bike_id = ride.bike_id;
            rider_id = ride.rider_id;
            route_id = ride.route_id;

            bike = ride.bike;
            rider = ride.rider;
            route = ride.route;

            distance = Math.Round(ride.distance, 2);
            reward = Math.Round(ride.reward, 2);
            notes = ride.notes;
            ride_date = ride.ride_date.ToString("dd/MM/yyyy");
        }
    }
}