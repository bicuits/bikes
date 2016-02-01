using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App
{
    public class AdminController : BikesControllerBase 
    {
        [HttpGet]
        public ActionResult Index()
        {
            AdminVM model = new AdminVM();

            model.archives = RideInfo.getArchiveRides();
            model.payments = Payment.getPayments();

            return View("Admin", model);
        }

        [HttpPost]
        public ActionResult Command(String command)
        {
            ActionResult result;

            switch (command)
            {
                case "archive":
                    archiveRides();
                    result = RedirectToAction("Index");
                    break;

                default:
                    result = RedirectToAction("Index", "Ride");
                    break;
            }

            return result;
        }

        private void archiveRides()
        {
            List<Ride> allRides = Ride.getRides();

            foreach (Rider rider in Rider.getRiders())
            {
                double total = 0;

                //get the rides for this rider
                IEnumerable<Ride> rides = allRides.Where(r => r.rider_id == rider.id);
                
                foreach (Ride ride in rides)
                {
                    //total the values of each ride
                    total += ride.rideValue;

                    //archive the ride
                    RideInfo.archiveRide(ride);
                }

                //make a payment record for this rider
                if (total > 0.01)
                {
                    Payment.makePayment(rider.name, total);
                }
            }
        }
    }
}