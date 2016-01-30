using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App
{
    public class RouteController : Controller
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
            Route route;

            switch (command)
            {
                case "save":
                    route = model.toRoute();
                    route.save();
                    break;

                case "delete":
                    if (model.id != Route.DefaultRouteId)
                    {
                        route = Route.getRoute(model.id);
                        route.deleted = true;
                        route.save();
                    }
                    break;

                default:
                    break;
            }

            return RedirectToAction("Index");
        }

    }
}