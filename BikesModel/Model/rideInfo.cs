using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Bikes.Model
{
    //Class models an archived ride
    [PetaPoco.TableName("ride_archive")]
    [PetaPoco.PrimaryKey("id")]
    public class RideInfo
    {
        public int id { get; internal set; }
        public DateTime ride_date { get; internal set; }
        public DateTime archive_date { get; internal set; }
        public double distance { get; internal set; }
        public double cash { get; internal set; }
        public bool return_ride { get; internal set; }
        public String notes { get; internal set; }
        public String bike { get; internal set; }
        public String rider { get; internal set; }
        public String route { get; internal set; }

        internal RideInfo()
        { }

        public static List<RideInfo> getArchiveRides()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            return db.Fetch<RideInfo>("");
       }

        public static RideInfo getArchiveRide(int id)
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            return db.FirstOrDefault<RideInfo>("WHERE id = @0", id);
        }

        internal void save()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            db.Save(this);
        }

        public static void archiveRide(Ride ride)
        {
            RideInfo info = new RideInfo();

            info.ride_date = ride.ride_date;
            info.archive_date = DateTime.Now;
            info.distance = ride.distance;
            info.cash = ride.rideValue;
            info.return_ride = ride.return_ride;
            info.notes = ride.notes == null ? "" : ride.notes ;
            info.bike = ride.bike == null ? "unknown" : ride.bike;
            info.rider = ride.rider;
            info.route = ride.route;

            //create an archive record
            info.save();

            //delete the original record
            Ride.deleteRide(ride.id);
        }
    }
}
