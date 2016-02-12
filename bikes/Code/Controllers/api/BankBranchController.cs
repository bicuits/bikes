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
    public class BankBranchController : BikesApiControllerBase
    {
        [HttpGet]
        public JArray Get()
        {
            JArray result = new JArray(
                Bank
                .listBranches()
                .Select(b => new JObject(
                    new JProperty("id", b.branchId.ToString()),
                    new JProperty("name", b.name))));

            BikesDebug.dumpToFile("branch-options.json", result.ToString(Newtonsoft.Json.Formatting.Indented));

            return result;
        }

    }
}
