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
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            else
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
                if (user.password == crypto.Compute(l.password, user.passwordSalt))
                {
                    FormsAuthentication.SetAuthCookie(user.userID.ToString(), l.RememberMe);
                    var ck = new HttpCookie("name", user.userID.ToString());
                    Response.Cookies.Add(ck);
                    HttpCookie cookie = HttpContext.Request.Cookies["name"];
                    SVTrade.LoggedUserInfo.SetLoggedUser(Convert.ToInt32(cookie.Value));
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Mapping", "MappingProducts/Mapping");
                    }
                }
            }
            else
            {
                ModelState.Remove("Password");
                ViewBag.Message = "Невірний логін або пароль";
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (Request.Cookies["name"] != null)
            {
                var c = new HttpCookie("name");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
