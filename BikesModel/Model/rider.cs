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
        public int? bank_branch_id { get; set; }
        public int? bank_customer_id { get; set; }
        //public string bank_username { get; set; }
        public int? bank_account_id { get; set; }

        public Rider()
        {
            //temporary
            bank_branch_id = 1;
        }

        public static List<Rider> getRiders(bool includeDeleted = false)
        {
            String whereClause = includeDeleted ? "" : "WHERE deleted = FALSE";
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
            List<Rider> riders = db.Fetch<Rider>(whereClause);

            return riders;
        }

        public static Rider getRider(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
            Rider rider = db.FirstOrDefault<Rider>("WHERE id = @0", id);

            return rider;
        }

        public static void deleteRider(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
            db.Execute("UPDATE rider SET deleted = TRUE WHERE id = @0", id);
        }
        public void save()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
            db.Save(this);
        }
    }
}