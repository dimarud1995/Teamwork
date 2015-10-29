using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SVTrade.Models;
using SVTrade.Abstract;

namespace SVTrade.Areas.Personal.Controllers
{
    public class UsersController : Controller
    {
        private TradeDBEntities db = new TradeDBEntities();
        private IRepository r;

        public UsersController(IRepository repo)
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies["name"];
                SVTrade.LoggedUserInfo.SetLoggedUser(SVTrade.LoggedUserInfo.currentUserId);
            }
            catch { }
            r = repo;
        }
        // GET: Personal/Users
        public ActionResult Index()
        {
            int tID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            var user = r.Users.FirstOrDefault(p => p.userID == tID);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Personal/Users/Details/5
        public ActionResult Details()
        {
            int id = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = r.Users.FirstOrDefault(p=>p.userID==id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Personal/Users/Create
        public ActionResult Create()
        {
            ViewBag.userGroupID = new SelectList(r.UserGroups, "userGroupID", "name");
            return View();
        }

        // POST: Personal/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,password,userGroupID,companyName,juridicalAddress,address,contactPerson,post,phoneNumber,email,merchantLicense,tradeLicense,approved,passwordSalt")] User user)
        {
            if (ModelState.IsValid)
            {
                r.SaveUser(user);
                return RedirectToAction("Index");
            }

            ViewBag.userGroupID = new SelectList(r.UserGroups, "userGroupID", "name", user.userGroupID);
            return View(user);
        }

        // GET: Personal/Users/Edit/5
        public ActionResult Edit()
        {
            int id = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = r.Users.FirstOrDefault(p=>p.userID==id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.userGroupID = new SelectList(r.UserGroups, "userGroupID", "name", user.userGroupID);
            return View("Index",user);
        }

        // POST: Personal/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,password,userGroupID,companyName,juridicalAddress,address,contactPerson,post,phoneNumber,email,merchantLicense,tradeLicense,approved,passwordSalt")] User user)
        {
            int tid= Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            User tempUser = r.Users.FirstOrDefault(p=>p.userID==tid);
            tempUser.companyName = user.companyName;
            tempUser.juridicalAddress = user.juridicalAddress;
            tempUser.address = user.address;
            tempUser.contactPerson = user.contactPerson;
            tempUser.post = user.post;
            tempUser.phoneNumber = user.phoneNumber;
            tempUser.approved = false;

            r.SaveUser(tempUser);
            
            
            ViewBag.userGroupID = new SelectList(r.UserGroups, "userGroupID", "name", user.userGroupID);
            return View("Index",tempUser);
        }

        // GET: Personal/Users/Delete/5
        public ActionResult Delete()
        {
            int id = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = r.Users.FirstOrDefault(p=>p.userID==id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Personal/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            int id = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            r.DeleteUser(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
