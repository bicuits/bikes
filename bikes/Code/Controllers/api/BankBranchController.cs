using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Bikes.Model.Banking;

namespace Bikes.Api
{
    public class BankBranchController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/bank/branch")]
        public IEnumerable<Bank.Branch> Get()
        {
            return Bank.listBranches();
        }
    }
}
