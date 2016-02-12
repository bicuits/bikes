using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Bikes.Model.Banking;
using System.Drawing;

namespace Bikes.App
{
    public class RiderVM
    {
        [HiddenInput]
        public int id { get; set; }
        [HiddenInput]
        public String bankBranchId { get; set; }
        [HiddenInput]
        public String bankCustomerId { get; set; }
        [HiddenInput]
        public String bankAccountId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "please enter the name of the rider")]
        public String name { get; set; }

        [Display(Name = "Pence per mile")]
        [Required(ErrorMessage = "please enter the rate per mile")]
        public int rate { get; set; }

        [Display(Name = "Chart colour")]
        public String chartColor { get; set; }

        public IEnumerable<Color> colorList
        {
            get
            {
                return Enum.GetValues(typeof(KnownColor))
                    .Cast<KnownColor>()
                    .Where(k => k >= KnownColor.Transparent && k < KnownColor.ButtonFace) //Exclude system colors
                    .Select(k => Color.FromKnownColor(k))
                    .OrderBy(c => c.GetHue())
                    .ThenBy(c => c.GetSaturation())
                    .ThenBy(c => c.GetBrightness());
            }
        }

        public RiderVM()
        {
            chartColor = ColorTranslator.ToHtml(Color.DarkOrange);
        }

        public RiderVM(Rider rider)
            : this()
        {
            id = rider.id;
            name = rider.name;
            rate = rider.rate;

            chartColor = rider.color_code;

            //convert null values to a default integer value (typically 0)
            bankBranchId = rider.bank_branch_id == null ? 
                Bank.DefaultBranchId.ToString() : 
                rider.bank_branch_id.ToString();

            bankCustomerId = rider.bank_customer_id == null ? 
                Bank.DefaultCustomerId.ToString() : 
                rider.bank_customer_id.ToString();

            bankAccountId = rider.bank_account_id == null ? 
                Bank.DefaultAccountId.ToString() : 
                rider.bank_account_id.ToString();
        }

        public Rider toRider()
        {
            Rider rider = new Rider();

            rider.id = id;
            rider.name= name;
            rider.rate = rate;
            //rider.color = System.Drawing.Color.FromArgb(255, red, green, blue);
            rider.color_code = chartColor;

            //store either an account number or null, discard any default "please select" type options generated 
            //by select lists which will have value DefaultxxxId
            rider.bank_branch_id = bankBranchId.ToNullableBankId(Bank.DefaultBranchId);
            rider.bank_customer_id = bankCustomerId.ToNullableBankId(Bank.DefaultCustomerId);
            rider.bank_account_id = bankAccountId.ToNullableBankId(Bank.DefaultAccountId);

            return rider;
        }

    }
}