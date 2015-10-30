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
    public class ProductToBuysController : Controller
    {
        private TradeDBEntities db = new TradeDBEntities();
        private IRepository r;

        public ProductToBuysController(IRepository repo)
        {
            r = repo;
        }
        // GET: Personal/ProductToBuys
        public ActionResult Index()
        {
            int tID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            var productToBuy = r.ProductsToBuy.Include(p => p.ProductCategory).Include(p => p.User).Where(p=>p.userID== tID);
            return View(productToBuy.ToList());
        }

        // GET: Personal/ProductToBuys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductToBuy productToBuy = r.ProductsToBuy.FirstOrDefault(p => p.productToBuyID == id);
            if (productToBuy == null)
            {
                return HttpNotFound();
            }
            return View(productToBuy);
        }

        // GET: Personal/ProductToBuys/Create
        public ActionResult Create()
        {
            ViewBag.productCategoryID = new SelectList(r.ProductCategories, "productCategoryID", "name");
            ViewBag.userID = new SelectList(r.Users, "userID", "userID");
            return View();
        }

        // POST: Personal/ProductToBuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productToBuyID,userID,title,productCategoryID,amount,price,description,approved")] ProductToBuy productToBuy)
        {
            productToBuy.userID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            if (ModelState.IsValid)
            {
                r.SaveProductToBuy(productToBuy);
             
                return RedirectToAction("Index");
            }

            ViewBag.productCategoryID = new SelectList(r.ProductCategories, "productCategoryID", "name", productToBuy.productCategoryID);
            ViewBag.userID = new SelectList(r.Users, "userID", "userID", productToBuy.userID);
            return View(productToBuy);
        }

        // GET: Personal/ProductToBuys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductToBuy productToBuy = r.ProductsToBuy.FirstOrDefault(p=>p.productToBuyID==id);
            if (productToBuy == null)
            {
                return HttpNotFound();
            }
            ViewBag.productCategoryID = new SelectList(r.ProductCategories, "productCategoryID", "name", productToBuy.productCategoryID);
            ViewBag.userID = new SelectList(r.Users, "userID", "userID", productToBuy.userID);
            return View(productToBuy);
        }

        // POST: Personal/ProductToBuys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productToBuyID,userID,title,productCategoryID,amount,price,description,approved")] ProductToBuy productToBuy)
        {
            productToBuy.userID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            if (ModelState.IsValid)
            {
                db.Entry(productToBuy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productCategoryID = new SelectList(r.ProductCategories, "productCategoryID", "name", productToBuy.productCategoryID);
            ViewBag.userID = new SelectList(r.Users, "userID", "userID", productToBuy.userID);
            return View(productToBuy);
        }

        // GET: Personal/ProductToBuys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductToBuy productToBuy = r.ProductsToBuy.FirstOrDefault(p=>p.productToBuyID==id);
            if (productToBuy == null)
            {
                return HttpNotFound();
            }
            return View(productToBuy);
        }

        // POST: Personal/ProductToBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
         
            r.DeleteProductToBuy(id);
            
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
