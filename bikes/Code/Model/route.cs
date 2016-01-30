using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Bikes.Model
{
    [PetaPoco.TableName("route")]
    [PetaPoco.PrimaryKey("id")]
    public class Route
    {
        public int id { get; set; }
        public String name { get; set; }
        public float distance { get; set; }
        public String notes { get; set; }
        public bool deleted { get; set; }

        public const int DefaultRouteId = 0;

        public static List<Route> getRoutes()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            List<Route> routes = db.Fetch<Route>("WHERE deleted = FALSE");

            return routes;
        }

        public static Route getRoute(int id)
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            Route route = db.FirstOrDefault<Route>("WHERE id = @0", id);

            return route;
        }

        //public static void deleteRoute(int id)
        //{
        //    Database db = new PetaPoco.Database("bikes-clunie");
        //    db.Execute("DELETE FROM route WHERE id = @0", id);
        //}

        public void save()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            db.Save(this);
        }
    }
}