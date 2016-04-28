using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Bikes.Model;
using Bikes.Model.Banking;

namespace Bikes.Api
{

    public class PaymentController : BikesApiControllerBase
    {
        [HttpGet]
        [Route("api/payment")]
        public JArray Get()
        {
            return new JArray(
                Payment.getPayments().Select(p => p.toJObject()));
        }

        //POST makes a payment for all riders
        [HttpPost]
        [Route("api/payment")]
        public List<Payment> Post()
        {
            IEnumerable<Rider> riders = Rider.getRiders(); ;
            IEnumerable<Ride> unpaidRides = Ride.getUnpaidRides();

            return payRiders(riders);
        }

        private List<Payment> payRiders(IEnumerable<Rider> riders)
        {
            IEnumerable<Ride> unpaidRides = Ride.getUnpaidRides();

            List<Payment> payments = new List<Payment>();

            foreach (Rider rider in riders)
            {
                Payment payment = payRider(unpaidRides, rider);
                if (payment != null)
                {
                    payments.Add(payment);
                }
            }
            return payments;
        }

        private Payment payRider(IEnumerable<Ride> unpaidRides, Rider rider)
        {
            Payment payment = null;
            IEnumerable<Ride> ridesToPay = unpaidRides.Where(r => r.rider_id == rider.id);

            decimal total = ridesToPay.Sum(r => r.reward);

            //if rider has a valid-looking account set up
            if (total > 0 &&
                rider.bank_branch_id.HasValue && 
                rider.bank_branch_id != Bank.DefaultBranchId &&
                rider.bank_customer_id.HasValue &&
                rider.bank_customer_id != Bank.DefaultCustomerId &&
                rider.bank_account_id.HasValue &&
                rider.bank_account_id != Bank.DefaultAccountId)
            {
                //pay into the bank
                payment = Bank.deposit(rider,
                    total,
                    "Bike mileage payment");

                if (payment.success)
                {
                    //mark each ride as paid
                    foreach (Ride ride in ridesToPay)
                    {
                        ride.setAsPaid(payment.id);
                    }
                }
                else
                {
                    //payment failed
                    //do nothing, don't return a payment record
                    payment = null;
                }
            }

            return payment;
        }
    }
}
