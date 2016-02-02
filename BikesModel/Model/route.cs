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

        public const int DefaultRouteId = 1;
        public const string DefaultRouteName = "Other";
        public const float DefaultRouteDistance = 0f;

        public static List<Route> getRoutes()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.Fetch<Route>("");
        }

        public static Route getRoute(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.FirstOrDefault<Route>("WHERE id = @0", id);
        }

        public static void deleteRoute(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Execute("DELETE FROM route WHERE id = @0", id);
        }

        public void save()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);

            if (id == Route.DefaultRouteId)
            {
                //cannot change name or distance for the default route
                name = DefaultRouteName;
                distance = DefaultRouteDistance;
            }
            db.Save(this);
        }
    }
}