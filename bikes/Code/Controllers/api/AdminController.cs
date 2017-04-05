using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bikes.Model;

namespace Bikes.Api
{
    public class AdminController : BikesApiControllerBase
    {
        [HttpPost]
        [Route("api/admin/routes/update")]
        public void updateRides()
        {
            Admin.updateRoutes();
        }
    }
}
