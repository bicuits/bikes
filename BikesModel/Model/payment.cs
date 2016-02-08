using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Bikes.Model.Banking
{
    //Payment is a record of payment made to the bank.  
    //These records are esssentially log files, they play no other role in the application
    [TableName("payment")]
    [PrimaryKey("id")]
    public class Payment
    {
        public const int NullPaymentId = 0;

        public int id { get; internal set; }
        public string rider { get; internal set; }
        public double amount { get; internal set; }
        public string bank_branch { get; internal set; }
        public string bank_username { get; internal set; }
        public string bank_account { get; internal set; }
        public DateTime? paid_date { get; internal set; }
        public bool success { get; internal set; }

        internal Payment()
        {
            id = NullPaymentId;
            success = false;
        }


        public static List<Payment> getPayments()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
            return db.Fetch<Payment>("");
        }

        public static Rider getPayment(int id)
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
            return db.FirstOrDefault<Rider>("WHERE id = @0", id);
        }

        //public static Payment recordPayment(Rider rider, double amount, string branch, string username, string account)
        //{
        //    Payment payment = new Payment();

        //    payment.rider = rider.name;
        //    payment.amount = amount;
        //    payment.paid_date = DateTime.Now;
        //    payment.bank_branch = branch;
        //    payment.bank_username = username;
        //    payment.bank_account = account;

        //    Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
        //    db.Insert(payment);

        //    return payment;
        //}

        //public static void deletePayment(int id)
        //{
        //    Database db = new PetaPoco.Database("bikes-clunie");
        //    db.Execute("DELETE FROM payment WHERE id = @0", id);
        //}

        internal void save()
        {
            Database db = new PetaPoco.Database(ModelConfig.connectionStringName("bikes"));
            db.Save(this);
        }
    }
}