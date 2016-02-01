using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bikes.App
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            LoginVM model = new LoginVM();

            return View("Login", model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (Membership.ValidateUser(model.username, model.password))
            {
                //log the user in
                FormsAuthentication.SetAuthCookie(model.username.ToLower(), false);
                return RedirectToAction("Index", "Ride");
            }
            else
            {
                model.errorMessage = "Failed to login.";
                return View("Login", model);
            }
        }
    }
}