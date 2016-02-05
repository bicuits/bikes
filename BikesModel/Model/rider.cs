using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using System.Drawing;

namespace Bikes.Model
{
    [PetaPoco.TableName("rider")]
    [PetaPoco.PrimaryKey("id")]
    public class Rider
    {
        public int id { get; set; }
        public String name { get; set; }
        public int rate { get; set; }
        public String color_code { get; set; } //strictly this should be stored in the app?
        public bool deleted { get; internal set; }

        [PetaPoco.Ignore]
        public Color color
        {
            get
            {
                return ColorTranslator.FromHtml(color_code);
            }
            set
            {
                color_code = ColorTranslator.ToHtml(value);
            }
        }

        public static List<Rider> getRiders()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            List<Rider> riders = db.Fetch<Rider>("WHERE deleted = FALSE");

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
            db.Execute("UPDATE ride SET deleted = TRUE WHERE id = @0", id);
        }
        public void save()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Save(this);
        }
    }
}