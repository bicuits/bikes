using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bikes.App
{
    public class RideVM
    {
        [HiddenInput]
        public int id { get; set; }

        [Display(Name = "Distance")]
        [Required]
        public double distance { get; set; }

        [Display(Name = "Return")]
        [Required]
        public bool returnRide { get; set; }

        [Display(Name = "Payable")]
        [Required]
        public bool payable { get; set; }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Text)]
        public String rideDate { get; set; }

        [Display(Name = "Route")]
        [Required]
        public String route { get; set; }

        [Display(Name = "Rider")]
        [Required]
        public String rider { get; set; }

        [Display(Name = "Bike")]
        public String bike { get; set; }

        [Display(Name = "Notes")]
        public String notes { get; set; }

        public int riderId { get; set; }
        public int routeId { get; set; }
        public int bikeId { get; set; }

        public IEnumerable<SelectListItem> bikeList
        {
            get
            {
                return new SelectList(Bike.getBikes(), "id", "name");
            }
        }

        public IEnumerable<SelectListItem> riderList
        {
            get
            {
                return new SelectList(Rider.getRiders(), "id", "name");
            }
        }

        public IEnumerable<SelectListItem> routeList
        {
            get
            {
                return new SelectList(Route.getRoutes(), "id", "name");
            }
        }

        public RideVM()
        {
            rideDate = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public RideVM(Ride ride)
            : this()
        {
            id = ride.id;

            bikeId = ride.bike_id;
            riderId = ride.rider_id;
            routeId = ride.route_id;

            distance = ride.distance;
            returnRide = ride.return_ride;
            payable = ride.payable;

            notes = ride.notes;
            rideDate = ride.ride_date.ToString("dd/MM/yyyy");

            //Following fields for display only, not persisted
            bike = ride.bike;
            rider = ride.rider;
            route = ride.route;
        }

        public Ride toRide()
        {
            Ride ride = new Ride();

            ride.id = id;

            ride.route_id = routeId;
            ride.bike_id = bikeId;
            ride.rider_id = riderId;

            ride.distance = distance;
            ride.return_ride = returnRide;
            ride.payable = payable;
            ride.notes = notes;
            ride.ride_date = DateTime.Parse(rideDate);

            return ride;
        }
    }
}