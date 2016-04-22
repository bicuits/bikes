using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bikes.App
{
    [Authorize(Roles = "user")]
    public class AppController : Controller
    {
        [HttpGet]
        [Route("app")]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}