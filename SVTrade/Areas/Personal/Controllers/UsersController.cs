﻿using System;
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
            r = repo;
        }
        // GET: Personal/Users
        public ActionResult Index()
        {
            int tID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            var user = r.Users.Include(u => u.UserGroup).Where(p=>p.userID== tID);
            return View(user.ToList());
        }

        // GET: Personal/Users/Details/5
        public ActionResult Details(int? id)
        {
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
        public ActionResult Edit(int? id)
        {
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
            return View(user);
        }

        // POST: Personal/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,password,userGroupID,companyName,juridicalAddress,address,contactPerson,post,phoneNumber,email,merchantLicense,tradeLicense,approved,passwordSalt")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userGroupID = new SelectList(r.UserGroups, "userGroupID", "name", user.userGroupID);
            return View(user);
        }

        // GET: Personal/Users/Delete/5
        public ActionResult Delete(int? id)
        {
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
        public ActionResult DeleteConfirmed(int id)
        {
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