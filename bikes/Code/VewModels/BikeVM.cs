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
    public class BikeVM
    {
        [HiddenInput]
        public int id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String name { get; set; }


        public BikeVM()
        {
        }

        public BikeVM(Bike bike)
            : this()
        {
            id = bike.id;
            name = bike.name;
        }

        public Bike toBike()
        {
            Bike bike = new Bike();

            bike.id = id;
            bike.name = name;
            return bike;
        }

    }
}