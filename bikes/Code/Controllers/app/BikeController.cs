using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App

{
    public class BikeController : BikesControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Bike> bikes = Bike.getBikes();
            return View("Index", bikes);
        }

        [HttpGet]
        public ActionResult Add()
        {
            BikeVM model = new BikeVM();

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Bike bike = Bike.getBike(id);
            BikeVM model = new BikeVM(bike);

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Command(String command, BikeVM model)
        {
            switch (command)
            {
                case "save":
                    Bike bike = model.toBike();
                    bike.save();
                    break;

                case "delete":
                    Bike.deleteBike(model.id);
                    break;

                default:
                    break;
            }

            return RedirectToAction("Index");
        }

    }
}