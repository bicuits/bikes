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
        public static void dumpToFile(String filename, String data)
        {
            if (BikesDebug.debug)
            {
                File.WriteAllText(HostingEnvironment.MapPath("~/App_Data/" + filename), data);
            }

        }

        public static bool debug
        {
            get
            {
                bool result = false;
                bool.TryParse(ConfigurationManager.AppSettings["debug"], out result);

                return result;
            }
        }
    }
}