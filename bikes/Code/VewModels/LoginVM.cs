using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bikes.App
{
    public class LoginVM
    {
        [Display(Name = "Username", Prompt="enter username...")]
        [Required(ErrorMessage = "please enter a user name")]
        public String username { get; set; }

        [Display(Name = "Password", Prompt = "password...")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "please enter a password")]
        public String password { get; set; }

        public String errorMessage { get; set; }
    }
}