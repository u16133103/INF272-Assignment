using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication18.Models;

namespace WebApplication18.Controllers
{
    public class UserDashController : Controller
    {
        [Authorize]
        // GET: UserDash
        public ActionResult CompanyProfile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Register");
        }

        
    }
}