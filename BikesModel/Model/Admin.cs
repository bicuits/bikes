using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bikes.Model
{
    public class Admin
    {
        public static void updateRoutes()
        {
            List < Route > routes = Route.getRoutes();

            foreach(Route route in routes)
            {
                calcPopularity(route);
                route.save();
            }
        } 

        private static void calcPopularity(Route route)
        {
            List<Ride> rides = Ride.getRidesForRoute(route.id);

            route.recent_rides = rides.Where(r => (DateTime.Now - r.ride_date).TotalDays < 60).Count();
        }
    }
}
