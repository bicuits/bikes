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
            return View("Index", rides.OrderByDescending(r => r.ride_date));
        }

        [HttpGet]
        public ActionResult Add()
        {
            RideVM model = new RideVM();

            return View("AddRide", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            RideVM model = new RideVM(Ride.getRide(id));

            return View("EditRide", model);
        }

        [HttpPost]
        public ActionResult Command(String command, RideVM model)
        {
            switch (command)
            {
                case "add":
                    Rider rider = Rider.getRider(model.riderId);
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

                    double rideValue = model.payable ? (rideLength * rider.rate) / 100f : 0;

                    Ride.add(
                        bike_id: model.bikeId,
                        rider_id: model.riderId,
                        route_id: model.routeId,
                        ride_date: DateTime.Parse(model.rideDate),
                        notes: model.notes,
                        reward: rideValue,
                        bonus: model.bonus,
                        distance: rideLength
                        );
                    break;

                case "save":
                    Ride.setNotes(model.id, model.notes);
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