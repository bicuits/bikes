using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App
{
    [Authorize(Roles = "user")]
    public class RouteController : BikesControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Route> routes = Route.getRoutes();
            return View("Index", routes);
        }

        [HttpGet]
        public ActionResult Add()
        {
            RouteVM model = new RouteVM();

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Route route = Route.getRoute(id);
            RouteVM model = new RouteVM(route);

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Command(String command, RouteVM model)
        {
            switch (command)
            {
                case "save":
                    Route route = model.toRoute();
                    route.save();
                    break;

                case "delete":
                    if (model.id != Route.DefaultRouteId)
                    {
                        Route.deleteRoute(model.id);
                    }
                    break;

                default:
                    break;
            }

            return RedirectToAction("Index");
        }

    }
}