using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bikes.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bikes.App
{
    public class RiderVM
    {
        [HiddenInput]
        public int id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "please enter the name of the rider")]
        public String name { get; set; }

        [Display(Name = "Pence per mile")]
        [Required(ErrorMessage = "please enter the rate per mile")]
        public int rate { get; set; }

        [Required(ErrorMessage = "please enter the colour for display in charts")]
        public int red { get; set; }

        [Required(ErrorMessage = "please enter the colour for display in charts")]
        public int green { get; set; }

        [Required(ErrorMessage = "please enter the colour for display in charts")]
        public int blue { get; set; }


        public RiderVM()
        {
            red = 220;
            green = 220;
            blue = 220; 
        }

        public RiderVM(Rider rider)
            : this()
        {
            id = rider.id;
            name = rider.name;
            rate = rider.rate;

            String[] colors = rider.color.Split(new char[] { ',' });

            red = int.Parse(colors[0]);
            green = int.Parse(colors[1]);
            blue = int.Parse(colors[2]);
        }

        public Rider toRider()
        {
            Rider rider = new Rider();

            rider.id = id;
            rider.name= name;
            rider.rate = rate;
            rider.color = String.Format("{0},{1},{2}", red, green, blue);
            return rider;
        }

    }
}