using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App
{
    public class RiderController : BikesControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Rider> riders = Rider.getRiders();
            return View("Index", riders);
        }

        [HttpGet]
        public ActionResult Add()
        {
            RiderVM model = new RiderVM();

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Rider rider = Rider.getRider(id);
            RiderVM model = new RiderVM(rider);

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Command(String command, RiderVM model)
        {
            switch (command)
            {
                case "save":
                    Rider rider = model.toRider();
                    rider.save();
                    break;

                case "delete":
                    Rider.deleteRider(model.id);
                    break;

                default:
                    break;
            }

            return RedirectToAction("Index");
        }
    }
}