using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;
using Bikes.Model.Banking;

namespace Bikes.App
{
    public class AdminController : BikesControllerBase 
    {
        [HttpGet]
        public ActionResult Index()
        {
            AdminVM model = new AdminVM();

            model.payments = Payment.getPayments();

            return View("Admin", model);
        }

        [HttpPost]
        public ActionResult Command(String command)
        {
            ActionResult result;

            switch (command)
            {
                case "pay":
                    payAll();
                    result = RedirectToAction("Index");
                    break;

                default:
                    result = RedirectToAction("Index", "Ride");
                    break;
            }

            return result;
        }

        private class Invoice
        {
            public int riderId { get; set; }
            public double amount { get; set; }
        }

        private void payAll()
        {
            IEnumerable<Rider> riders = Rider.getRiders(includeDeleted: true);
            IEnumerable<Ride> unpaidRides = Ride.getUnpaidRides();

            foreach (var group in unpaidRides.GroupBy(r => r.rider_id))
            {
                Rider rider = riders.Where(r => r.id == group.Key).First();
                double total = group.Sum(r => r.reward);

                //if rider has a valid-looking account set up
                if (rider.bank_username != null &&
                    rider.bank_username.Length > 0 &&
                    rider.bank_account_id != Bank.DefaultAccountId)
                {
                    //pay into the bank
                    Payment payment = Bank.deposit(rider,
                        total,
                        "Bike mileage payment");

                    if (payment.success)
                    {
                        //mark each ride as paid
                        foreach (Ride ride in group)
                        {
                            ride.setAsPaid(payment.id);
                        }
                    }
                    else
                    {
                        //payment failed
                        //do nothing
                    }
                }
                else
                {
                    //money owing but no account set up
                    //do nothing
                }
            }
        }
    }
}