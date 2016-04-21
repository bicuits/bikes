using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bikes.Model.Banking;

namespace Bikes.Api
{
    public class BankCustomerController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/bank/branch/{id:int}/customer")]
        public IEnumerable<Bank.Customer> Get(int id)
        {
            return Bank.listCustomersForBranch(id);
        }
    }
}
