using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bikes.App
{
    public class HomeController : BikesControllerBase
    {
        [HttpGet]
        public ActionResult Home()
        {
            return View("Home");
        }
    }
}