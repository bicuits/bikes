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
        public int id { get; set; }                 //primary key
        public int bike_id { get; set; }            //foreign key, which bike was ridden
        public int rider_id { get; set; }           //foreign key, who rode the bike
        public int route_id { get; set; }           //foreign key, what route did they ride
        public DateTime ride_date { get; set; }     //date the ride was ridden
        public double distance { get; set; }        //distance ridden, only used for route type "other"
        public bool return_ride { get; set; }       //was the route ridden one-way or there and back
        public bool payable { get; set; }           //does the ride have a cash value
        public String notes { get; set; }           //user defined field

        //read-only properties
        [PetaPoco.ResultColumn]
        public String bike { get; internal set; }   //for display - name of the bike ridden
        [PetaPoco.ResultColumn]
        public String rider { get; internal set; }  //for display - name of the rider
        [PetaPoco.ResultColumn]
        public String route { get; internal set; }  //for display - name of the route ridden
        [PetaPoco.ResultColumn]
        public int rate { get; internal set; }      //for display or calculation - pence per mile
        [PetaPoco.ResultColumn]
        public double routeLength{ get; internal set; }  //for display or calculation - length of the route (one-way only)
        [PetaPoco.Ignore]
        public double rideLength                   //for display or calculation - the total distance ridden
        {
            get
            {
                if (route_id == Route.DefaultRouteId)
                {
                    return distance;
                }
                else if (return_ride)
                {
                    return routeLength * 2;
                }
                else
                {
                    return routeLength;
                }
            }
        }                            
        [PetaPoco.Ignore]
        public double rideValue                  //for display or calculation - the cash value of the ride
        {
            get
            {
                return payable ? (rideLength * rate) / 100f : 0f;
            }
        }

        //utility function to build the sql statement to perform the join
        private static Sql sql()
        {
            return Sql
                .Builder
                .Append("SELECT a.id, a.distance, a.return_ride, a.ride_date, a.payable, a.bike_id, a.rider_id, a.route_id, a.notes,")
                .Append("b.name AS route, b.distance AS routeLength, c.name AS rider, c.rate AS rate, d.name AS bike")
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
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Execute("DELETE FROM ride WHERE id = @0", id);
        }

        public void save()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Save(this);
        }
    }
}