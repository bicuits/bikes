using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bikes.Model
{
    public static class ModelConfig
    {
        public static String connectionStringName(string databaseName)
        {
            //example names are "bikes-live", "bikes-test", "bikes-clunie", "bikes-local"
            return String.Format("{0}-{1}", databaseName, ConfigurationManager.AppSettings["connection"]);
        }

        public static String connectionString(string databaseName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName(databaseName)].ConnectionString;
        }

    }
}
