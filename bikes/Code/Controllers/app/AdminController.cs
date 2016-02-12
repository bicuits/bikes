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
        public ActionResult IndexA()
        {
            return View("AdminA");
        }


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

                    //had this over to the API controller
                    var controller = DependencyResolver.Current.GetService<PaymentController>();
                    controller.Post();

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