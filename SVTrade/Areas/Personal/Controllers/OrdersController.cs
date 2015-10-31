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
using System.IO;

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
            int tid;
            try
            {
                tid = Convert.ToInt32(HttpContext.Request.Cookies["name"].Value);
            }
            catch { tid = 0; }
            var order = r.Orders.Include(o => o.OrderStatus).Include(o => o.Product).Include(o => o.User).Where(p=>p.userID==tid);
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
            Order tempOrder = r.Orders.FirstOrDefault(p=>p.orderID==order.orderID);
     
            tempOrder.finishDate = order.finishDate;
           
            tempOrder.amount = order.amount;

           

            if (ModelState.IsValid)
            {
                r.SaveOrder(tempOrder);
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
