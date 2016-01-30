using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Bikes.Model
{
    [PetaPoco.TableName("ride")]
    [PetaPoco.PrimaryKey("id")]
    public class Ride
    {
        public int id { get; set; }

        public int bike_id { get; set; }
        public int rider_id { get; set; }
        public int route_id { get; set; }

        [PetaPoco.ResultColumn]
        public String bike { get; set; }
        [PetaPoco.ResultColumn]
        public String rider { get; set; }
        [PetaPoco.ResultColumn]
        public String route { get; set; }
        [PetaPoco.ResultColumn]
        public int rate { get; set; }

        [PetaPoco.Ignore]
        public double amount
        {
            get
            {
                return (distance * rate) / 100f;
            }
        }


        public double distance { get; set; }
        public String notes { get; set; }
        public DateTime ride_date { get; set; }

        public static List<Ride> getRides()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            List<Ride> rides = db.Fetch<Ride>(
                PetaPoco.Sql.Builder
                    .Append("SELECT a.id, a.route_id, a.rider_id, a.bike_id, a.distance, a.ride_date, a.notes, a.last_updated,")
                    .Append("b.name AS route, c.name AS rider, c.rate AS rate, d.name AS bike")
                    .Append("FROM ride a, route b, rider c, bike d")
                    .Append("WHERE a.route_id = b.id AND a.rider_id = c.id AND a.bike_id = d.id"));

            return rides;
        }

        public static Ride getRide(int id)
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            Ride ride = db.FirstOrDefault<Ride>(
                PetaPoco.Sql.Builder
                    .Append("SELECT a.id, a.route_id, a.rider_id, a.bike_id, a.distance, a.ride_date, a.notes, a.last_updated,")
                    .Append("b.name AS route, c.name AS rider, c.rate AS rate, d.name AS bike")
                    .Append("FROM ride a, route b, rider c, bike d")
                    .Append("AND a.id = @0", id));

            return ride;
        }

        public static void deleteRide(int id)
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            db.Execute("DELETE FROM ride WHERE id = @0", id);
        }

        public void save()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            db.Save(this);
        }
    }
}