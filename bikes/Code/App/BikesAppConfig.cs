using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;

namespace Bikes.App
{
    public static class BikesAppConfig
    {
        public static bool debug
        {
            get
            {
                //settings are "true", "false"
                return bool.Parse(WebConfigurationManager.AppSettings["debug"]);
            }
        }
    }
}