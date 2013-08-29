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
            var members = Membership.FindUsersByEmail(email);
            var enu = members.GetEnumerator();
            if (enu.MoveNext())
            {
                var firstUser = enu.Current as MembershipUser;

                if (Membership.ValidateUser(firstUser.UserName, password))
                {
                    FormsAuthentication.SetAuthCookie(firstUser.UserName, false);
                }
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
