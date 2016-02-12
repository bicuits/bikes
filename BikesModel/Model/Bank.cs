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
        public const int DefaultCustomerId = 0;
        public const int DefaultBranchId = 0;
        public const int DefaultAccountId = 0;
        public const int DrurysLedgerId = 1;
        public const int DrurysBranchId = 2;

        public enum UserRoles
        {
            none = 0,
            customer = 1,
            manager = 2,
            administrator = 3
        }

        private enum TranTypes
        {
            transfer = 0,
            reversal = 1,
            pocketMoney = 2,
            interest = 3
        }

        public class Branch
        {
            public int branchId { get; internal set; }
            public String name { get; internal set; }

            internal Branch() { }
        }

        public class Customer
        {
            public int customerId { get; internal set; }
            public int branchId { get; internal set; }
            public String username { get; internal set; }

            internal Customer() { }
        }

        public class Account
        {
            public int id { get; internal set; }
            public int holderId { get; internal set; }
            public String name { get; internal set; }

            internal Account() { }
        }

        public static List<Branch> listBranches()
        {
            List<Branch> branches = new List<Branch>();

            //branches.Add(
            //    new Branch()
            //    {
            //        branchId = DefaultBranchId,
            //        name = "other"
            //    });

            branches.Add(
                new Branch()
                {
                    branchId = 2,
                    name = "Drumtochty Branch"
                });

            return branches;
        }

        public static List<Customer> listCustomersForBranch(int branchId)
        {
            List<Customer> list = new List<Customer>();
            //list.Add(
            //    new Customer()
            //    {
            //        customerId = DefaultCustomerId,
            //        branchId = branchId,
            //        username = "other"
            //    });


            using (MySqlConnection conn = new MySqlConnection(ModelConfig.connectionString("bank")))
            {
                MySqlCommand command = new MySqlCommand("listCustomers", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("_branch_id", branchId);

                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if ((UserRoles)(int)reader["role_id"] == UserRoles.customer)
                    {
                        Customer cust = new Customer();
                        //must have
                        cust.customerId = (int)reader["customer_id"];
                        cust.branchId = (int)reader["branch_id"];
                        cust.username = (String)reader["username"];

                        list.Add(cust);
                    }
                }
                reader.Close();
                conn.Close();
            }

            return list;
        }

        public static List<Account> listAccountsForCustomer(int customerId)
        {
            List<Account> accounts = new List<Account>();

            //accounts.Add(new Account()
            //{
            //    id = DefaultAccountId,
            //    holderId = DefaultCustomerId,
            //    name = "other"
            //});

            using (MySqlConnection conn = new MySqlConnection(ModelConfig.connectionString("bank")))
            {
                MySqlCommand command = new MySqlCommand("listAccountsForCustomer", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("_customer_id", customerId);

                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Account account = new Account();
                    account.id = (int)reader["account_id"];
                    account.holderId = (int)reader["holder_id"];
                    account.name = (string)reader["account_name"];

                    accounts.Add(account);
                }
                reader.Close();
                conn.Close();
            }

            return accounts;
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
                Customer holder = getCustomer(rider.bank_customer_id.Value);
                Account destAccount = getAccount(rider.bank_account_id.Value);

                //Check that the account belongs to the user.  Remove if other branches are to be allowed.
                //Also need to implement getBranch() and use dynamic data for branch info.
                if (destAccount.holderId != holder.customerId)
                {
                    throw new ApplicationException("Cannot pay into bank.  User is not an account holder.");
                }

                Account ledger = getLedger(holder.branchId);

                //hard coded check that were are using drurys branch ledger, 
                //remove this check if the app is to be used for other branches
                if (ledger.id != DrurysLedgerId)
                {
                    throw new ApplicationException("Attempt to post transaction to non-drurys branch");
                }

                //post the transaction to the bank database
                postTran(ledger.id, destAccount.id, (int)Math.Floor(amount * 100), description);

                payment.success = true;

                try
                {
                    //create a record of the transaction in the bikes database
                    payment.rider = rider.name;
                    payment.amount = amount;
                    payment.paid_date = DateTime.Now;
                    payment.bank_branch = "Drumtochty Branch";
                    payment.bank_username = holder.username;
                    payment.bank_account = destAccount.name;

                    payment.save();
                }
                catch
                {
                    //still report success even if we have failed to log the payment, otherwise
                    //it will keep on getting paid again on any further retries
                }
            }
            catch (Exception e)
            {
                payment.success = false;
            }

            return payment;
        }

        private static Customer getCustomer(int customerId)
        {
            Customer customer = null;

            using (MySqlConnection conn = new MySqlConnection(ModelConfig.connectionString("bank")))
            {
                MySqlCommand command = new MySqlCommand("getCustomer", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("_customer_id", customerId);

                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customer = new Customer();
                    customer.customerId = (int)reader["customer_id"];
                    customer.branchId = (int)reader["branch_id"];
                    customer.username = reader["username"] as String;
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
                    account.name = reader["account_name"] as String;
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