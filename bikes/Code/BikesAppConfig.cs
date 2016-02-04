using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Bikes.App
{
    public static class BikesAppConfig
    {
        public static bool debug
        {
            get
            {
                //settings are "true", "false"
                return bool.Parse(ConfigurationManager.AppSettings["debug"]);
            }
        }

    }
}