using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        public double reward { get; set; }
        public String rideDate { get; set; }
        public String notes { get; set; }

        //The following are transient fields, only used for creating rides
        public bool returnRide { get; set; }
        public bool payable { get; set; }
        public int riderId { get; set; }
        public int routeId { get; set; }
        public int bikeId { get; set; }
        public double bonus { get; set; }

        public RideVM()
        {
            bikeId = 0;
            riderId = 0;
            routeId = 0;

            bike = "other";
            rider = "other";
            route = "other";

            returnRide = false;
            payable = true;

            rideDate = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public RideVM(Ride ride)
            : this()
        {
            id = ride.id;

            bike = ride.bike;
            rider = ride.rider;
            route = ride.route;
            distance = ride.distance;
            reward = ride.reward;
            notes = ride.notes;
            rideDate = ride.ride_date.ToString("dd/MM/yyyy");
        }
    }
}