using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Bikes.Model.Banking
{
    public class Bank
    {
        public const int DefaultAccountId = 0;
        public const int DrurysLedgerId = 1;

        private enum TranTypes
        {
            transfer = 0,
            reversal = 1,
            pocketMoney = 2,
            interest = 3
        }

        private class Customer
        {
            public int customerId { get; set; }
            public int branchId { get; set; }
            public int roleId { get; set; }
        }

        private class Account
        {
            public int id { get; set; }
            public int holderId { get; set; }
        }

        public static Payment deposit(Rider rider, double amount, string description)
        {
            Payment payment = new Payment();

            //TO DO: lots of hard-coding here.  Also this really should transact with the bank app rather 
            //than poking values into the database directly.  At the momen the bank app does not support
            //this sort of interaction

            try
            {
                //first get the account and check it is the drurys branch
                Customer holder = getCustomerFromUsername(rider.bank_username);
                Account destAccount = getAccount(rider.bank_account_id);

                //check that the account belongs to the user
                if (destAccount.holderId != holder.customerId)
                {
                    throw new ApplicationException("Cannot pay into bank.  User is not an account holder.");
                }

                Account ledger = getLedger(holder.branchId);

                //hard coded check that were are using drurys branch ledger
                //TO DO: make this chack dynamic from the rider's branch data property
                if (ledger.id != DrurysLedgerId)
                {
                    throw new ApplicationException("Attempt to post transaction to non-drurys branch");
                }

                //post the transaction to the bank database
                postTran(ledger.id, destAccount.id, (int)Math.Floor(amount * 100), description);

                //create a record of the transaction in the bikes database
                payment.rider = rider.name;
                payment.amount = amount;
                payment.paid_date = DateTime.Now;
                payment.bank_branch = rider.bank_branch_id.ToString();
                payment.bank_username = rider.bank_username;
                payment.bank_account = rider.bank_account_id.ToString();

                payment.success = true;
                payment.save();
            }
            catch (Exception e)
            {
                payment.success = false;
            }

            return payment;
        }

        private static Customer getCustomerFromUsername(string username)
        {
            Customer customer = null;

            using (MySqlConnection conn = new MySqlConnection(ModelConfig.connectionString("bank")))
            {
                MySqlCommand command = new MySqlCommand("getCustomerFromUsername", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("_username", username);

                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customer = new Customer();
                    customer.customerId = (int)reader["customer_id"];
                    customer.branchId = (int)reader["branch_id"];
                }
                reader.Close();
                conn.Close();
            }

            return customer;
        }
        private static Account getAccount(int accountId)
        {
            Account account = null;

            using (MySqlConnection conn = new MySqlConnection(ModelConfig.connectionString("bank")))
            {
                MySqlCommand command = new MySqlCommand("getAccount", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("_account_id", accountId);

                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    account = new Account();
                    account.id = (int)reader["account_id"];
                    account.holderId = (int)reader["holder_id"];
                }
                reader.Close();
                conn.Close();
            }

            return account;
        }
        private static Account getLedger(int branchId)
        {
            Account account = null;

            using (MySqlConnection conn = new MySqlConnection(ModelConfig.connectionString("bank")))
            {
                MySqlCommand command = new MySqlCommand("getLedger", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("_branch_id", branchId);

                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    account = new Account();
                    account.id = (int)reader["account_id"];
                    account.holderId = (int)reader["holder_id"];
                }
                reader.Close();
                conn.Close();
            }

            return account;
        }

        private static void postTran (int srcAccountId, int destAccountId, int amount, string description)
        { 
            using (MySqlConnection conn = new MySqlConnection(ModelConfig.connectionString("bank")))
            {
                MySqlCommand command = new MySqlCommand("postTran", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("_src_account_id", srcAccountId);
                command.Parameters.AddWithValue("_dest_account_id", destAccountId);
                command.Parameters.AddWithValue("_amount", amount);
                command.Parameters.AddWithValue("_tran_type", (int)TranTypes.transfer);
                command.Parameters.AddWithValue("_tran_date", DateTime.Now);
                command.Parameters.AddWithValue("_description", description);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}