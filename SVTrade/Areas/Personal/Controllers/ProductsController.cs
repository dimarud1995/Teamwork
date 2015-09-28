using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SVTrade.Models;

namespace SVTrade.Areas.Personal.Controllers
{
    public class ProductsController : Controller
    {
        private TradeDBEntities db = new TradeDBEntities();

        // GET: Personal/Products
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.ProductCategory).Include(p => p.User);
            return View(product.ToList());
        }

        // GET: Personal/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Personal/Products/Create
        public ActionResult Create()
        {
            ViewBag.productCategoryID = new SelectList(db.ProductCategory, "productCategoryID", "name");
            ViewBag.userID = new SelectList(db.User, "userID", "password");
            return View();
        }

        // POST: Personal/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,name,productCategoryID,imageURL,amount,price,description,userID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productCategoryID = new SelectList(db.ProductCategory, "productCategoryID", "name", product.productCategoryID);
            ViewBag.userID = new SelectList(db.User, "userID", "password", product.userID);
            return View(product);
        }

        // GET: Personal/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productCategoryID = new SelectList(db.ProductCategory, "productCategoryID", "name", product.productCategoryID);
            ViewBag.userID = new SelectList(db.User, "userID", "password", product.userID);
            return View(product);
        }

        // POST: Personal/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,name,productCategoryID,imageURL,amount,price,description,userID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productCategoryID = new SelectList(db.ProductCategory, "productCategoryID", "name", product.productCategoryID);
            ViewBag.userID = new SelectList(db.User, "userID", "password", product.userID);
            return View(product);
        }

        // GET: Personal/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Personal/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
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
