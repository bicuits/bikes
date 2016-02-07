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
        //read/write properites persisted to the database
        public int id { get; internal set; }                 //primary key

        public int bike_id { get; internal set; }            //foreign key, which bike was ridden
        public int rider_id { get; internal set; }           //foreign key, who rode the bike
        public int route_id { get; internal set; }           //foreign key, what route did they ride
        public int payment_id { get; internal set; }         //foreign key, hwhere was the reward paid

        public String bike { get; internal set; }            //name of the bike ridden
        public String rider { get; internal set; }           //name of the rider
        public String route { get; internal set; }           //name of the route ridden

        public DateTime ride_date { get; internal set; }     //date the ride was ridden
        public String notes { get; internal set; }           //user defined field
        public double reward { get; internal set; }          //earned value in Pounds
        public double distance { get; internal set; }        //length of the route (including return trip)

        internal Ride()
        {
            payment_id = Payment.NullPaymentId;
        }

        //utility function to build the sql statement to perform the join
        private static Sql sql()
        {
            return Sql
                .Builder
                .Append("SELECT a.id, a.bike_id, a.rider_id, a.route_id, a.ride_date, a.notes, a.reward, a.distance,")
                .Append("b.name AS route, c.name AS rider, d.name AS bike")
                .Append("FROM ride a, route b, rider c, bike d")
                .Append("WHERE a.route_id = b.id AND a.rider_id = c.id AND a.bike_id = d.id");
        }

        public static List<Ride> getRides()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.Fetch<Ride>(sql());
        }

        public static Ride getRide(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.FirstOrDefault<Ride>(sql().Append("AND a.id = @0", id));
        }

        public static void deleteRide(int id)
        {
            Ride ride = getRide(id);

            //do not allow rides that have been paid to be deleted
            if (ride != null && ride.payment_id == Payment.NullPaymentId)
            {
                Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
                db.Execute("DELETE FROM ride WHERE id = @0", id);
            }
        }

        public static void add(
                        int bike_id,
                        int rider_id,
                        int route_id,
                        DateTime ride_date,
                        String notes,
                        double reward,
                        double distance)
        {
            Ride ride = new Ride();

            ride.bike_id = bike_id;
            ride.rider_id = rider_id;
            ride.route_id = route_id;

            //if no names supplied then look up from ids
            ride.bike = Bike.getBike(bike_id).name;
            ride.rider = Rider.getRider(rider_id).name;
            ride.route = Route.getRoute(route_id).name;
            ride.ride_date = ride_date;

            ride.notes = notes;
            ride.reward = reward;
            ride.distance = distance;

            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Insert(ride);
        }
    }
}