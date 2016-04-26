using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bikes.Api
{
    //RideVM is a view model for the Ride class.
    //Ride objects are mostly immutable.  Only the notes field can be updated once a ride is created.
    public class PwdVM
    {
        public string pwd { get; set; }
    }
}