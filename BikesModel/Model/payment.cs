using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Bikes.Model
{
    [TableName("payment")]
    [PrimaryKey("id")]
    public class Payment
    {
        public int id { get; set; }
        public String rider { get; set; }
        public double amount { get; set; }
        public DateTime created_date { get; set; }
        public DateTime? paid_date { get; set; }

        public const int NullPaymentId = 0;

        internal Payment() { }

        public static List<Payment> getPayments()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.Fetch<Payment>("");
        }

        public static Rider getPayment(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            return db.FirstOrDefault<Rider>("WHERE id = @0", id);
        }

        public static void makePayment(String rider, double amount)
        {
            Payment payment = new Payment();

            payment.rider = rider;
            payment.amount = amount;
            payment.created_date = DateTime.Now;
            payment.paid_date = null;
            
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Save(payment);
        }

        //public static void deletePayment(int id)
        //{
        //    Database db = new PetaPoco.Database("bikes-clunie");
        //    db.Execute("DELETE FROM payment WHERE id = @0", id);
        //}

        public void save()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName);
            db.Save(this);
        }
    }
}