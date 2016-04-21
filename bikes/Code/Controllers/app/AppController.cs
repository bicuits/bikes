using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bikes.App
{
    public class AppController : BikesControllerBase
    {
        [HttpGet]
        [Route("app")]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}