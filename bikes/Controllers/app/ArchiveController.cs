using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;

namespace Bikes.App
{
    public class ArchiveController : BikesControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<RideInfo> rides = RideInfo.getArchiveRides();

            return View("Index", rides);
        }
    }
}