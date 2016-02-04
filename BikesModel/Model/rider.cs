using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Bikes.Model
{
    [PetaPoco.TableName("rider")]
    [PetaPoco.PrimaryKey("id")]
    public class Rider
    {
        public int id { get; set; }
        public String name { get; set; }
        public int rate { get; set; }
        public String color { get; set; } //strictly this should be stored in the app?

        public static List<Rider> getRiders()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            List<Rider> riders = db.Fetch<Rider>("");

            return riders;
        }

        public static Rider getRider(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            Rider rider = db.FirstOrDefault<Rider>("WHERE id = @0", id);

            return rider;
        }

        public static void deleteRider(int id)
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