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
        public bool deleted { get; set; }
        public int rate { get; set; }

        public static List<Rider> getRiders()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            List<Rider> riders = db.Fetch<Rider>("WHERE deleted = FALSE");

            return riders;
        }

        public static Rider getRider(int id)
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            Rider rider = db.FirstOrDefault<Rider>("WHERE id = @0", id);

            return rider;
        }

        public void save()
        {
            Database db = new PetaPoco.Database("bikes-clunie");
            db.Save(this);
        }
    }
}