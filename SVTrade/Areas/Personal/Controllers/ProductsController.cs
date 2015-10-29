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
    public class ProductsController : Controller
    {
        private TradeDBEntities db = new TradeDBEntities();
        private IRepository r;

        public ProductsController(IRepository repo)
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies["name"];
                SVTrade.LoggedUserInfo.SetLoggedUser(Convert.ToInt32(cookie.Value));
            }
            catch { }
            r = repo;
        }
        // GET: Personal/Products
        public ActionResult Index()
        {
            int tID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            var product = r.Products.OrderBy(p=>p.title).Include(p => p.ProductCategory).Include(p => p.User).Where(p=>p.userID== tID);
            ViewBag.Sorting = "1";
            return View(product.ToList());
        }

        // GET: Personal/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = r.Products.FirstOrDefault(p => p.productID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //string[] tempString = Directory.GetFiles(product.imageURL);
            // ViewBag.URL = System.IO.Path.Combine(product.imageURL,tempString[0] );
            ViewBag.URL = "<img src=\"" + product.imageURL +"\"/>" ;
            ViewBag.URL1 = product.imageURL;
            return View(product);
        }

        // GET: Personal/Products/Create
        public ActionResult Create()
        {
            ViewBag.productCategoryID = new SelectList(r.ProductCategories, "productCategoryID", "name");
            ViewBag.userID = new SelectList(db.Users, "userID", "userID");
            return View();
        }

        // POST: Personal/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,title,productCategoryID,imageURL,amount,price,description,userID,approved")] Product product)
        {
            product.userID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            product.approved = false;
            HttpPostedFileBase photo = Request.Files["photo"];
            String LocalAdress = "~/Areas/Personal/Pictures";
            String ProjPart = System.IO.Path.Combine(Server.MapPath(LocalAdress),product.userID.ToString(),product.productCategoryID.ToString(),product.productID.ToString());
            String LocalFullAdress = System.IO.Path.Combine(ProjPart, System.IO.Path.GetFileName(photo.FileName));
            System.IO.Directory.CreateDirectory(ProjPart);
            String PartAdress = System.IO.Path.Combine("~/Areas/Personal/Pictures", product.userID.ToString(), product.productCategoryID.ToString(), product.productID.ToString(), System.IO.Path.GetFileName(photo.FileName));
            PartAdress = PartAdress.Trim(' ');
            
            if (photo.FileName!="")
            {
                product.imageURL = PartAdress;
                photo.SaveAs(LocalFullAdress);
            }
            else
            {
                product.imageURL = "Добавте картинку";
            }
           
            if (ModelState.IsValid)
            {
                r.SaveProduct(product);
               
                return RedirectToAction("Index");
            }

            ViewBag.productCategoryID = new SelectList(db.ProductCategories, "productCategoryID", "name", product.productCategoryID);
            ViewBag.userID = new SelectList(db.Users, "userID", "userID", product.userID);
            return View(product);
        }
  
        // GET: Personal/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = r.Products.FirstOrDefault(p=>p.productID==id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productCategoryID = new SelectList(r.ProductCategories, "productCategoryID", "name", product.productCategoryID);
            ViewBag.userID = new SelectList(r.Users, "userID", "userID", product.userID);
            return View(product);
        }

        // POST: Personal/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,title,productCategoryID,imageURL,amount,price,description")] Product product)
        {
            product.userID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            product.approved = false;
            HttpPostedFileBase photo = Request.Files["photo"];
            String LocalAdress = "~/Areas/Personal/Pictures";
            String ProjPart = System.IO.Path.Combine(Server.MapPath(LocalAdress), product.userID.ToString(), product.productCategoryID.ToString(), product.productID.ToString());
            String LocalFullAdress = System.IO.Path.Combine(ProjPart, System.IO.Path.GetFileName(photo.FileName));
            System.IO.Directory.CreateDirectory(ProjPart);
            String PartAdress = System.IO.Path.Combine("~/Areas/Personal/Pictures", product.userID.ToString(), product.productCategoryID.ToString(), product.productID.ToString(), System.IO.Path.GetFileName(photo.FileName));
            PartAdress = PartAdress.Trim(' ');

            if (photo.FileName != "")
            {
                product.imageURL = PartAdress;
                photo.SaveAs(LocalFullAdress);
            }
            else
            {
                product.imageURL = r.Products.FirstOrDefault(p => p.productID == product.productID).imageURL;
            }
           
            
            if (ModelState.IsValid)
            {
                r.SaveProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.productCategoryID = new SelectList(r.ProductCategories, "productCategoryID", "name", product.productCategoryID);
            ViewBag.userID = new SelectList(r.Users, "userID", "userID", product.userID);
            return View(product);
        }

        // GET: Personal/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = r.Products.FirstOrDefault(p=>p.productID== id);
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
            r.DeleteProduct(id);
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
        public ActionResult SortProductBy()
        {
            String type = Request["SortType"];
            
            int tID = Convert.ToInt32(SVTrade.LoggedUserInfo.currentUserId);
            
            if (type=="За назвою")
            {
               var  product = r.Products.OrderBy(p => p.title).Include(p => p.ProductCategory).Include(p => p.User).Where(p => p.userID == tID);
                ViewBag.Sorting = 1;
                return View("Index", product.ToList());
            }
            else
            {
                if (type == "За ціною")
                {
                    var product = r.Products.OrderBy(p => p.price).Include(p => p.ProductCategory).Include(p => p.User).Where(p => p.userID == tID);
                    ViewBag.Sorting = 2;
                    return View("Index", product.ToList());
                }
                else
                {
                    var product = r.Products.OrderBy(p => p.ProductCategory.name).Include(p => p.ProductCategory).Include(p => p.User).Where(p => p.userID == tID);
                    ViewBag.Sorting = 3;
                    return View("Index", product.ToList());
                }
            }
            
        }
    }
}
