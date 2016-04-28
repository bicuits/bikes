using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App
{
    [Authorize(Roles = "user")]
    public class AppController : Controller
    {
        [HttpGet]
        [Route("app")]
        public ActionResult Index()
        {
            AppVM model = new AppVM();

            Rider rider = Rider.findRiderByName(User.Identity.Name);
            if (rider != null)
            {
                model.currentRiderId = rider.id;
                model.currentRiderName = rider.name;
            }
            else
            {
                model.currentRiderId = 0;
                model.currentRiderName = User.Identity.Name;
            }

            return View("Index", model);
        }
    }
}