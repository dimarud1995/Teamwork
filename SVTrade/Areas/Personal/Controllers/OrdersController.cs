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
    public class OrdersController : Controller
    {
        private TradeDBEntities db = new TradeDBEntities();
        private IRepository r;
        public OrdersController(IRepository repo)
        {
            r = repo;
        }

        // GET: Personal/Orders
        public ActionResult Index()
        {
            int tID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            var order = r.Orders.Include(o => o.OrderStatus).Include(o => o.Product).Include(o => o.User).Where(p=>p.userID== tID);
            return View(order.ToList());
        }

        // GET: Personal/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = r.Orders.FirstOrDefault(p=>p.orderID==id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Personal/Orders/Create
        public ActionResult Create()
        {
            ViewBag.statusID = new SelectList(r.OrderStatuses, "statusID", "name");
            ViewBag.productID = new SelectList(r.Products, "productID", "title");
            ViewBag.userID = new SelectList(r.Users, "userID", "password");
            return View();
        }

        // POST: Personal/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderID,orderDate,finishDate,productID,userID,amount,statusDate,statusID,completed,canceled")] Order order)
        {
            order.userID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            if (ModelState.IsValid)
            {
                r.SaveOrder(order);
                return RedirectToAction("Index");
            }

            ViewBag.statusID = new SelectList(r.OrderStatuses, "statusID", "name", order.statusID);
            ViewBag.productID = new SelectList(r.Products, "productID", "title", order.productID);
            ViewBag.userID = new SelectList(r.Users, "userID", "password", order.userID);
            return View(order);
        }

        // GET: Personal/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = r.Orders.FirstOrDefault(p=>p.orderID==id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.statusID = new SelectList(r.OrderStatuses, "statusID", "name", order.statusID);
            ViewBag.productID = new SelectList(r.Products, "productID", "title", order.productID);
            ViewBag.userID = new SelectList(r.Users, "userID", "password", order.userID);
            return View(order);
        }

        // POST: Personal/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderID,orderDate,finishDate,productID,userID,amount,statusDate,statusID,completed,canceled")] Order order)
        {
            order.userID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            order.statusID = 1;
            order.canceled = false;
            order.completed = false;
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.statusID = new SelectList(r.OrderStatuses, "statusID", "name", order.statusID);
            ViewBag.productID = new SelectList(r.Products, "productID", "title", order.productID);
            ViewBag.userID = new SelectList(r.Users, "userID", "password", order.userID);
            return View(order);
        }

        // GET: Personal/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = r.Orders.FirstOrDefault(p=>p.orderID==id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Personal/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            r.DeleteOrder(id);
            
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
