using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bikes.Model;

namespace Bikes.App
{
    public class AdminVM
    {
        public IEnumerable<RideInfo> archives { get; set; }
        public IEnumerable<Payment> payments { get; set; }
    }
}