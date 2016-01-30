using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App

{
    public class BikeController : Controller
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
            Bike bike;

            switch (command)
            {
                case "save":
                    bike = model.toBike();
                    bike.save();
                    break;

                case "delete":
                    bike = Bike.getBike(model.id);
                    bike.deleted = true;
                    bike.save();
                    break;

                default:
                    break;
            }

            return RedirectToAction("Index");
        }

    }
}