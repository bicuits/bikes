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

        public const int DefaultId = 1;
        public const string DefaultName = "Other";
        public bool deleted { get; internal set; }

        public static List<Bike> getBikes()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.Fetch<Bike>("WHERE deleted = FALSE");
        }

        public static Bike getBike(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.FirstOrDefault<Bike>("WHERE id = @0", id);
        }

        public static void deleteBike(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Execute("UPDATE bike SET deleted = TRUE WHERE id = @0", id);
        }

        public void save()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            if (id == Route.DefaultId)
            {
                //cannot change name for the default bike
                name = DefaultName;
            }
            db.Save(this);
        }
    }
}