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

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Ride ride = Ride.getRide(id);
            RideVM model = new RideVM(ride);

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Command(String command, RideVM model)
        {
            switch (command)
            {
                case "save":
                    Ride ride = model.toRide();
                    ride.save();
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