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
                    result = RedirectToAction("Index");
                    break;

                default:
                    result = RedirectToAction("Index", "Ride");
                    break;
            }

            return result;
        }
    }
}