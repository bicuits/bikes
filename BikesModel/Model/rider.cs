using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace Bikes.Model
{
    [PetaPoco.TableName("rider")]
    [PetaPoco.PrimaryKey("id")]
    internal class PwdPoco
    {
        public int id { get; set; }
        public string pwd { get; set; }
    }

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
        public int? bank_account_id { get; set; }
        public bool default_payable { get; set; }

        public Rider()
        {
            //temporary
            bank_branch_id = 1;
        }

        public static List<Rider> getRiders(bool includeDeleted = false)
        {
            String whereClause = includeDeleted ? "" : "WHERE deleted = FALSE";
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                List<Rider> riders = db.Fetch<Rider>(whereClause);
                return riders;
            }
        }

        public static Rider getRider(int id)
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                Rider rider = db.FirstOrDefault<Rider>("WHERE id = @0", id);
                return rider;
            }
        }

        public static Rider findRiderByName(string name)
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                Rider rider = db.FirstOrDefault<Rider>("WHERE name = @0", name);
                return rider;
            }
        }

        public static string getPwd(int id)
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                PwdPoco poco = db.FirstOrDefault<PwdPoco>("WHERE id = @0", id);
                return poco.pwd;
            }
        }

        public void setPwd(string newPwd)
        {
            RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();

            HashAlgorithm algorithm = SHA256.Create();

            byte[] src = Encoding.Unicode.GetBytes(newPwd);
            byte[] hash = algorithm.ComputeHash(src);

            string pwd = Convert.ToBase64String(hash);

            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                db.Execute("UPDATE rider SET pwd=@1 WHERE id = @0", id, pwd);
            }
        }

        public static void deleteRider(int id)
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                db.Execute("UPDATE rider SET deleted = TRUE WHERE id = @0", id);
            }
        }

        public void save()
        {
            using (Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes")))
            {
                db.Save(this);
            }
        }
    }
}