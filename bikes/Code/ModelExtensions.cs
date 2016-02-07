using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bikes.Model;

namespace Bikes.App
{
    public static class ModelExtensions
    {
        public static String notesSummary(this Ride ride)
        {
            return getSummary(ride.notes);
        }

        private static String getSummary(String src)
        {
            String result;
            const int maxLen = 30;

            if (src == null)
            {
                result = "";
            }
            else if (src.Length < maxLen)
            {
                result = src;
            }
            else
            {
                result = src.Substring(0, maxLen - 1) + "...";
            }
            return result;

        }
    }
}