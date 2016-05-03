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
        //returns a name for the connection
        public static String connectionName()
        {
            //example names are "live", "test", "clunie", "local"
            return ConfigurationManager.AppSettings["connection"];
        }

        //returns the name of the connection string for the connection
        public static String connectionStringName(string databaseName)
        {
            //example names are "bikes-live", "bikes-test", "bikes-clunie", "bikes-local"
            return String.Format("{0}-{1}", databaseName, connectionName());
        }

        //returns an actual connection string
        //the string will vary depending what connection name is in teh config, eg "live" or "clunie"
        public static String connectionString(string databaseName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName(databaseName)].ConnectionString;
        }

    }
}
