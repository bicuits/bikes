using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bikes.Model.Banking;

namespace Bikes.App
{
    public class BankCustomerController : BikesApiControllerBase
    {
        [HttpGet]
        public JArray Get(int id)
        {
            JArray result = new JArray(
                Bank
                .listCustomersForBranch(id)
                .Select(c => new JObject(
                    new JProperty("id", c.customerId.ToString()),
                    new JProperty("name", c.username))));

            BikesDebug.dumpToFile("customer-options.json", result.ToString(Newtonsoft.Json.Formatting.Indented));

            return result;
        }
    }
}
