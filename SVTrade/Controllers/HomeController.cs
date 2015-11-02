using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVTrade.Models;
using SVTrade.Abstract;
using System.Web.Mvc;

namespace SVTrade.Controllers
{

    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            this.repository = repo;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }

        [Authorize] // This is for Authorize user
        public ActionResult MyProfile()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult UserIndex()
        {
            return View();
        }

        public ViewResult ManagerIndex()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Директор")]
        public ActionResult ManagerIndex(string password)
        {
            if (password == "2325")
            {
                SVTrade.LoggedUserInfo.HardReset(password);
                var c = new HttpCookie("name");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                return RedirectToAction("Logout", "Account");
            }
            return View();
        }



        [Authorize(Roles = "ConfirmedUser")]
        public ActionResult ConfirmedUserIndex()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}

