using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Home page for " + User.Identity.Name;
            }
            else
            {
                ViewBag.Title = "Home page for guest user.";
            }

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Title = "Register Page";

            return View();
        }

    }
}
