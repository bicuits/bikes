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
    public class RouteVM
    {
        [HiddenInput]
        public int id { get; set; }

        [Display(Name = "Description")]
        [Required]
        public String name { get; set; }

        [Display(Name = "Distance")]
        [Required]
        public float distance { get; set; }

        [Display(Name = "Notes")]
        public String notes { get; set; }

        public RouteVM()
        {
        }

        public RouteVM(Route route)
            : this()
        {
            id = route.id;
            name = route.name;
            distance = route.distance;
            notes = route.notes;
        }

        public Route toRoute()
        {
            Route route = new Route();

            route.id = id;

            route.name = name;
            route.distance = distance;

            route.notes = notes;
            return route;
        }
    }
}