using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bikes.Model.Banking;

namespace Bikes.Api
{
    public class BankAccountController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/bank/customer/{id:int}/account")]
        public IEnumerable<Bank.Account> Get(int id)
        {
            return Bank.listAccountsForCustomer(id);
        }

    }
}
