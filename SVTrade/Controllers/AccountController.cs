using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SVTrade.Models;
using SVTrade.Abstract;
using System.Web.Security;

namespace SVTrade.Controllers
{
    public class AccountController : Controller
    {
              


        private IRepository repository;


        public AccountController(IRepository repo)
        {
            repository = repo;
        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l, string ReturnUrl = "")
        {
            var crypto = new SimpleCrypto.PBKDF2();
            var user = repository.Users.Where(a => a.email.Equals(l.email)).FirstOrDefault();
            if (user != null)
            {
                user.password = crypto.Compute(l.password, user.passwordSalt);
                FormsAuthentication.SetAuthCookie(Convert.ToString(user.userID), l.RememberMe);
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("MyProfile", "Home");
                }
            }

            ModelState.Remove("Password");
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
     


    }
}
