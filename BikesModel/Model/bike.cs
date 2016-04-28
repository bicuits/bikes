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
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                return db.Fetch<Bike>("WHERE deleted = FALSE");
            }
        }

        public static Bike getBike(int id)
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                return db.FirstOrDefault<Bike>("WHERE id = @0", id);
            }
        }

        public static void deleteBike(int id)
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                db.Execute("UPDATE bike SET deleted = TRUE WHERE id = @0", id);
            }
        }

        public void save()
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                if (id == Bike.DefaultId)
                {
                    //cannot change name for the default bike
                    name = DefaultName;
                }
                db.Save(this);
            }
        }
    }
}