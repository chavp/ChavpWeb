using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string email, string password)
        {
            if (Membership.ValidateUser(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, false);
            }


            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}
