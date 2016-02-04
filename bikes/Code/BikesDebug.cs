using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Hosting;
using System.IO;

namespace Bikes.App
{
    public static class BikesDebug
    {
        public static void dumpToFile(String Filename, String data)
        {
            if (BikesDebug.debug)
            {
                File.WriteAllText(HostingEnvironment.MapPath("~/App_Data/riderSummary.json"), data);
            }

        }

        public static bool debug
        {
            get
            {
                bool unused;
                return bool.TryParse(ConfigurationManager.AppSettings["debug"], out unused);
            }
        }
    }
}