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
        public static String connectionStringName
        {
            get
            {
                //settings are "live", "test", "clunie", "local"
                return String.Format("bikes-{0}", ConfigurationManager.AppSettings["connection"]);
            }
        }
    }
}
