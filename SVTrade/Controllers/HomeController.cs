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
        static public int CurrentUserId;
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            try
            {
                CurrentUserId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            }
            catch { }
            this.repository = repo;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                int currentUserGroup = 1;
                ViewData["CurrentUser"] = from User in repository.Users where User.userID == CurrentUserId select User;

                foreach (var a in (IEnumerable<SVTrade.Models.User>)ViewData["CurrentUser"])
                {
                    currentUserGroup = a.userGroupID;
                }

                ViewData["CurrentUserGroup"] = from UserGroup in repository.UserGroups where UserGroup.userGroupID == currentUserGroup select UserGroup;
            }
            catch { }
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



        [Authorize(Roles = "Manager")]
        public ActionResult ManagerIndex()
        {
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

