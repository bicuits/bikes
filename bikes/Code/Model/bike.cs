using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Bikes.Model
{
    [PetaPoco.TableName("bike")]
    [PetaPoco.PrimaryKey("id")]
    public class Bike
    {
        public int id { get; set; }
        public String name { get; set; }
        public bool deleted { get; set; }

        public static List<Bike> getBikes()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            List<Bike> bikes = db.Fetch<Bike>("WHERE deleted = FALSE");

            return bikes;
        }

        public static Bike getBike(int id)
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            Bike bike = db.FirstOrDefault<Bike>("WHERE id = @0", id);

            return bike;
        }

        //public static void deleteBike(int id)
        //{
        //    Database db = new PetaPoco.Database("bikes-clunie");
        //    db.Execute("DELETE FROM bike WHERE id = @0", id);
        //}

        public void save()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            db.Save(this);
        }
    }
}