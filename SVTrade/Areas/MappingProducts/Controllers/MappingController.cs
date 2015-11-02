using SVTrade.Abstract;
using SVTrade.Areas.MappingProducts.Models;
using SVTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SVTrade.Areas.MappingProducts.Controllers
{
    public class MappingController : Controller
    {
        private SessionStateTempDataProvider session;
        private IRepository repository;
        public MappingController(IRepository repo)
        {
        
            this.repository = repo;
        }
        static private IEnumerable<Product> AllProd;
        static private IEnumerable<Product> AllProd1;
        #region Products
        public  ViewResult Mapping(int? currentCutegory, string returnUrl)
        {
            IEnumerable<SVTrade.Models.Product> ProductForUser = from Product in repository.Products  select Product;
            AllProd = repository.Products.Where(a =>
            a.productID == 0);
            if (currentCutegory.HasValue)
            {
                ProductForUser = ProductForUser.Where(a => a.productCategoryID == currentCutegory);
                AllProd = ProductForUser;
            }
            else
                AllProd = ProductForUser;
            var showedProductId = repository.ShowedProducts.Where(a => a.userID == 1);

            SelectList bCateg = new SelectList(repository.ProductCategories, "productCategoryID", "name");
            ViewData["Name"] = repository.ProductCategories.OrderBy(p => p.productCategoryID);
            ViewData["Product"] = ProductForUser;
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies["name"];
                ViewBag.UserID = Convert.ToInt64(cookie.Value);
            }
            catch { }
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public ViewResult Mapping(double? firstP, double? secondP, string nameOfProduct, string returnUrl)
        {
            ViewData["Name"] = repository.ProductCategories.OrderBy(p => p.productCategoryID);
            if (!String.IsNullOrEmpty(nameOfProduct))
            {
                AllProd1 = AllProd.Where(a => a.title == nameOfProduct);
                ViewData["Name"] = repository.ProductCategories.OrderBy(p => p.productCategoryID);
                ViewData["Product"] = AllProd1;
                return View(new CartIndexViewModel
                {
                    Cart = GetCart(),
                    ReturnUrl = returnUrl
                });
            }
            else
            {
                if (firstP.HasValue && secondP.HasValue)
                {
                    AllProd1 = AllProd.Where(a => a.price >= firstP && a.price <= secondP);
                }
                else
                    if (firstP.HasValue)
                {
                    AllProd1 = AllProd.Where(a => a.price >= firstP);
                }
                else
                        if (secondP.HasValue)
                {
                    AllProd1 = AllProd.Where(a => a.price <= secondP);
                }
                else
                    AllProd1 = AllProd;

                ViewData["Name"] = repository.ProductCategories.OrderBy(p => p.productCategoryID);
                ViewData["Product"] = AllProd1;
                try {
                    

                    ViewBag.UserID = Convert.ToInt64(HttpContext.Request.Cookies["name"].Value);
                }
                catch { }

            return View(new CartIndexViewModel
                {
                    Cart = GetCart(),
                    ReturnUrl = returnUrl
                });
            }
        }

        #endregion
        #region Cart
        public RedirectToRouteResult AddToCart(int productId, string returnUrl, int pluser = 0)
        {
            SVTrade.Models.Product product = repository.Products
            .FirstOrDefault(p => p.productID == productId);
            if (product != null)
            {
                GetCart().AddItem(product, Convert.ToInt32(HttpContext.Request.Cookies["name"].Value), pluser);
            }
            return RedirectToAction("Mapping", new { returnUrl });
        }

        public async Task<RedirectToRouteResult> AddToOrder(string returnUrl)
        {

                TradeDBEntities _db = new TradeDBEntities();
                
                SVTrade.Models.Order SetOrder = new SVTrade.Models.Order();
            try
            {
                var NewOrder = Cart.lineCollection.Where(a => a.idUser.Equals(Convert.ToInt32(HttpContext.Request.Cookies["name"].Value)));
                foreach (var a in NewOrder)
                {
                    SetOrder.amount = a.Product.amount;
                    SetOrder.productID = a.Product.productID;
                    SetOrder.userID = Convert.ToInt32(HttpContext.Request.Cookies["name"].Value);
                    SetOrder.orderDate = DateTime.Today;
                    SetOrder.finishDate = DateTime.Today.AddMonths(1);
                    SetOrder.statusDate = DateTime.Today;
                    SetOrder.canceled = false;
                    SetOrder.completed = false;
                    SetOrder.statusID = 1;
                    _db.Orders.Add(SetOrder);
                    await _db.SaveChangesAsync();

                }

            }
            catch { }
        
            Cart.lineCollection.Clear();
            return RedirectToAction("Mapping", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {

            SVTrade.Models.Product product = repository.Products
            .FirstOrDefault(p => p.productID == productId);
            if (product != null)
            {
                GetCart().RemoveLine(product, Convert.ToInt32(HttpContext.Request.Cookies["name"].Value));
            }
            return RedirectToAction("Mapping", new { returnUrl });
        }
        public RedirectToRouteResult AddItem(SVTrade.Models.Product product, int? pluser, string returnUrl)
        {

            CartLine line = Cart.lineCollection
              .Where(p => p.Product.productID == product.productID && p.idUser == Convert.ToInt32(HttpContext.Request.Cookies["name"].Value))
              .FirstOrDefault();

            if (line == null)
            {

            }
            else
                if (pluser > 0)
            {

                line.Product.amount = Convert.ToInt16(pluser);

            }
            return RedirectToAction("Mapping", new { returnUrl });

        }
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        #endregion

        #region PreOrder

        public PartialViewResult CreatePreOrder()
        {

            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                        select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };
            
                return PartialView();
         
        }
        public ActionResult AddPreOrder( ProductToBuy preOrder)
        {
            preOrder.userID = Convert.ToInt32(HttpContext.Request.Cookies["name"].Value);
            preOrder.approved = false;
            repository.SaveProductToBuy(preOrder);
            return RedirectToAction("CreatePreOrder");
        }
            #endregion
        }
}