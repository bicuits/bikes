using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App
{
    public class RideController : BikesControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Ride> rides = Ride.getRides();
            return View("Index", rides.OrderBy(r => r.ride_date));
        }

        [HttpGet]
        public ActionResult Add()
        {
            RideVM model = new RideVM();

            return View("AddRide", model);
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            RideVM model = new RideVM(Ride.getRide(id));

            return View("ViewRide", model);
        }

        [HttpPost]
        public ActionResult Command(String command, RideVM model)
        {
            switch (command)
            {
                case "save":

                    double rideLength;

                    if (model.routeId == Route.DefaultId)
                    {
                        rideLength = model.distance;
                    }
                    else
                    {
                        rideLength = Route.getRoute(model.routeId).distance;
                        if (model.returnRide)
                        {
                            rideLength *= 2;
                        }
                    }

                    double rideValue = model.payable ? (rideLength * Rider.getRider(model.riderId).rate) / 100f : 0f;

                    Ride.add(
                        bike_id: model.bikeId,
                        rider_id: model.riderId,
                        route_id: model.routeId,
                        ride_date: DateTime.Parse(model.rideDate),
                        notes: model.notes,
                        reward: rideValue,
                        distance: rideLength
                        );
                    break;

                case "delete":
                    Ride.deleteRide(model.id);
                    break;

                default:
                    break;
            }

            return RedirectToAction("Index");
        }
    }
}