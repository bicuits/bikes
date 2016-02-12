using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Bikes.Model.Banking;

namespace Bikes.App
{
    public class BankAccountController : BikesApiControllerBase
    {
        [HttpGet]
        public JArray Get(int id)
        {
            JArray result = new JArray(
                Bank
                .listAccountsForCustomer(id)
                .Select(a => new JObject(
                    new JProperty("id", a.id.ToString()),
                    new JProperty("name", a.name))));

            BikesDebug.dumpToFile("account-options.json", result.ToString(Newtonsoft.Json.Formatting.Indented));

            return result;
        }

    }
}
