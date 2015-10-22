using System;
using System.Linq;
using System.Web.Mvc;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/
        private IRepository repository;
 
        public AdminController(IRepository repo)
        {
            repository = repo;
        }
        
        //+ D
        #region UserGroup

        [Authorize(Roles = "Директор")]
        public ViewResult UserGroupsList()
        {
            return View(repository.UserGroups);
        }
        [Authorize(Roles = "Директор")]
        
        public ViewResult EditUserGroup(int userGroupID)
        {
            var userGroup = repository.UserGroups.FirstOrDefault(p => p.userGroupID == userGroupID);
            return View(userGroup);
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult EditUserGroup(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                repository.SaveUserGroup(userGroup);
                TempData["message"] = string.Format("Посаду {0} було збережено!", userGroup.name);
                return RedirectToAction("UserGroupsList");
            }
            else
            {
                return View(userGroup);
            }
        }
        [Authorize(Roles = "Директор")]
        public ViewResult CreateUserGroup()
        {
            return View("EditUserGroup", new UserGroup());
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult DeleteUserGroup(int userGroupID)
        {
            var deleted = repository.DeleteUserGroup(userGroupID);
            if (deleted != null)
                TempData["message"] = string.Format("Посаду {0} було видалено!", deleted.name);
            return RedirectToAction("UserGroupsList");
        }
        #endregion
        //+ D
        #region ProductCategory
        [Authorize(Roles = "Директор")]
        public ViewResult ProductCategoriesList()
        {
            return View(repository.ProductCategories);
        }
        [Authorize(Roles = "Директор")]
        public ViewResult EditProductCategory(int productCategoryID)
        {
            var category = repository.ProductCategories.FirstOrDefault(p => p.productCategoryID == productCategoryID);
            return View(category);
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult EditProductCategory(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProductCategory(productCategory);
                TempData["message"] = string.Format("Категрію {0} було збережено!", productCategory.name);

                return RedirectToAction("ProductCategoriesList");
            }
            else
            {
                return View(productCategory);
            }
        }
        [Authorize(Roles = "Директор")]
        public ViewResult CreateProductCategory()
        {
            return View("EditProductCategory", new ProductCategory());
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult DeleteProductCategory(int productCategoryID)
        {
            var deleted = repository.DeleteProductCategory(productCategoryID);
            if (deleted != null)
                TempData["message"] = string.Format("Категорію {0} було видалено!", deleted.name);
            return RedirectToAction("ProductCategoriesList");
        }


        #endregion
        //+ D,M
        #region Article
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult ArticlesList()
        {
            return View(repository.Articles);
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult EditArticle(int articleID)
        {
            var article = repository.Articles.FirstOrDefault(p => p.articleID == articleID);

            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString()};

            return View(article);
        }
        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                article.date = DateTime.Now;
                repository.SaveArticle(article);
                TempData["message"] = string.Format("Новину {0} було збережено!", article.title);

                return RedirectToAction("ArticlesList");
            }
            else
            {
                return View(article);
            }
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult CreateArticle()
        {
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            return View("EditArticle", new Article());
        }
        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult DeleteArticle(int articleID)
        {
            var deleted = repository.DeleteArticle(articleID);
            if (deleted != null)
                TempData["message"] = string.Format("Новину {0} було видалено!", deleted.title);
            return RedirectToAction("ArticlesList");
        }


        #endregion //+
        //+ D
        #region User
        [Authorize(Roles = "Директор")]
        public ViewResult UsersList()
        {
            return View(repository.Users);
        }
        [Authorize(Roles = "Директор")]
        public ViewResult EditUser(int userID)
        {
            var user = repository.Users.FirstOrDefault(p => p.userID == userID);

            ViewData["AllGroup"] = from item in repository.UserGroups.AsEnumerable()
                    select new SelectListItem {Text = item.name, Value = item.userGroupID.ToString()};
            return View(user);
        }

        [Authorize(Roles = "Директор")]

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            
            if (ModelState.IsValid)
            {
                repository.SaveUser(user);
                TempData["message"] = string.Format("Користувача {0} було збережено!", user.contactPerson);

                return RedirectToAction("UsersList");
            }
            else
            {
                return View(user);
            }
        }
        [Authorize(Roles = "Директор")]
        public ViewResult CreateUser()
        {
            ViewData["AllGroup"] = from item in repository.UserGroups.AsEnumerable()
                                   select new SelectListItem { Text = item.name, Value = item.userGroupID.ToString() };
            return View("EditUser", new User());
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult DeleteUser(int userID)
        {
            var deleted = repository.DeleteUser(userID);
            if (deleted != null)
                TempData["message"] = string.Format("Користувача {0} було видалено!", deleted.contactPerson);
            return RedirectToAction("UsersList");
        }


        #endregion
        //+ D
        #region Product
        [Authorize(Roles = "Директор")]
        public ViewResult ProductsList()
        {
            return View(repository.Products);
        }
        [Authorize(Roles = "Директор")]
        public ViewResult EditProduct(int productID)
        {
            var product = repository.Products.FirstOrDefault(p => p.productID == productID);

            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                   select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };

            return View(product);
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult EditProduct(Product product) 
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Товар {0} було збережено!", product.title);

                return RedirectToAction("ProductsList");
            }
            else
            {
                return View(product);
            }
        }
        [Authorize(Roles = "Директор")]
        public ViewResult CreateProduct()
        {
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                        select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };

            return View("EditProduct", new Product());
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult DeleteProduct(int productID)
        {
            var deleted = repository.DeleteProduct(productID);
            if (deleted != null)
                TempData["message"] = string.Format("Товар {0} було видалено!", deleted.title);
            return RedirectToAction("ProductsList");
        }


        #endregion
        //+ D,M
        #region Order
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult OrdersList()
        {
            return View(repository.Orders);
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult EditOrder(int orderID)
        {
            var order = repository.Orders.FirstOrDefault(p => p.orderID == orderID);

            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllProducts"] = from item in repository.Products.AsEnumerable()
                                   select new SelectListItem { Text = item.title, Value = item.productID.ToString() };
            ViewData["AllStatuses"] = from item in repository.OrderStatuses.AsEnumerable()
                                   select new SelectListItem { Text = item.name, Value = item.statusID.ToString() };
           

            return View(order);
        }


        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult EditOrder(Order order)
        {

            if (ModelState.IsValid)
            {
                repository.SaveOrder(order);
                TempData["message"] = string.Format("Замовлення {0} було збережено!", order.orderID);

                return RedirectToAction("OrdersList");
            }
            else
            {
                return View(order);
            }
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult CreateOrder()
        {
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllProducts"] = from item in repository.Products.AsEnumerable()
                                      select new SelectListItem { Text = item.title, Value = item.productID.ToString() };
            ViewData["AllStatuses"] = from item in repository.OrderStatuses.AsEnumerable()
                                      select new SelectListItem { Text = item.name, Value = item.statusID.ToString() };
           
            
            return View("EditOrder", new Order());
        }
        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult DeleteOrder(int orderID)
        {
            var deleted = repository.DeleteOrder(orderID);
            if (deleted != null)
                TempData["message"] = string.Format("Замовлення {0} було видалено!", deleted.orderID);
            return RedirectToAction("OrdersList");
        }


        #endregion
        //+ D
        #region ShowedProduct
        [Authorize(Roles = "Директор")]
        public ViewResult ShowedProductsList()
        {
            return View(repository.ShowedProducts);
        }
        [Authorize(Roles = "Директор")]
        public ViewResult EditShowedProduct(int showedProductID)
        {
            var showedProduct = repository.ShowedProducts.FirstOrDefault(p => p.showedProductID == showedProductID);

            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllProducts"] = from item in repository.Products.AsEnumerable()
                                      select new SelectListItem { Text = item.title, Value = item.productID.ToString() };
            return View(showedProduct);
        }


        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult EditShowedProduct(ShowedProduct showedProduct)
        {

            if (ModelState.IsValid)
            {
                repository.SaveShowedProduct(showedProduct);
                TempData["message"] = string.Format("Показаний товар {0} було збережено!", showedProduct.showedProductID);

                return RedirectToAction("ShowedProductsList");
            }
            else
            {
                return View(showedProduct);
            }
        }
        [Authorize(Roles = "Директор")]
        public ViewResult CreateShowedProduct()
        {
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllProducts"] = from item in repository.Products.AsEnumerable()
                                      select new SelectListItem { Text = item.title, Value = item.productID.ToString() };
            return View("EditShowedProduct", new ShowedProduct());
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult DeleteShowedProduct(int showedProductID)
        {
            var deleted = repository.DeleteShowedProduct(showedProductID);
            if (deleted != null)
                TempData["message"] = string.Format("Показаний товар {0} було видалено!", deleted.showedProductID);
            return RedirectToAction("ShowedProductsList");
        }


        #endregion
        //+ D
        #region ChoosedCategory
        [Authorize(Roles = "Директор")]
        public ViewResult ChoosedCategoriesList()
        {
            return View(repository.ChoosedCategories);
        }
        [Authorize(Roles = "Директор")]
        public ViewResult EditChoosedCategory(int chosenCategoryID)
        {
            var user = repository.ChoosedCategories.FirstOrDefault(p => p.chosenCategoryID == chosenCategoryID);

            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                        select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                      select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            return View(user);
        }


        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult EditChoosedCategory(ChoosedCategory choosedCategory)
        {

            if (ModelState.IsValid)
            {
                repository.SaveChoosedCategory(choosedCategory);
                TempData["message"] = string.Format("Обрану категорію {0} було збережено!", choosedCategory.chosenCategoryID);

                return RedirectToAction("ChoosedCategoriesList");
            }
            else
            {
                return View(choosedCategory);
            }
        }
        [Authorize(Roles = "Директор")]
        public ViewResult CreateChoosedCategory()
        {
            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                        select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
          
            return View("EditChoosedCategory", new ChoosedCategory());
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult DeleteChoosedCategory(int choosenCategoryID)
        {
            var deleted = repository.DeleteChoosedCategory(choosenCategoryID);
            if (deleted != null)
                TempData["message"] = string.Format("Обрану категорію {0} було видалено!", deleted.chosenCategoryID);
            return RedirectToAction("ChoosedCategoriesList");
        }


        #endregion
        //+ M
        #region For manager User
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult ManagerUsersList()
        {

            return View(repository.Users.Where(x => x.approved == false).AsEnumerable());
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult EditManagerUser(int userID)
        {
            var user = repository.Users.FirstOrDefault(p => p.userID == userID);

            ViewData["AllGroup"] = from item in repository.UserGroups.AsEnumerable()
                                   select new SelectListItem { Text = item.name, Value = item.userGroupID.ToString() };
            return View(user);
        }


        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult EditManagerUser(User user)
        {

            if (ModelState.IsValid)
            {
                repository.SaveUser(user);
                TempData["message"] = string.Format("Користувача {0} було збережено!", user.contactPerson);

                return RedirectToAction("ManagerUsersList");
            }
            else
            {
                return View(user);
            }
        }


        #endregion //manager 
        //+ D
        #region OrderStatus
        [Authorize(Roles = "Директор")]
        public ViewResult OrderStatusesList()
        {
            return View(repository.OrderStatuses);
        }
        [Authorize(Roles = "Директор")]
        public ViewResult EditOrderStatus(int statusID)
        {
            var user = repository.OrderStatuses.FirstOrDefault(p => p.statusID == statusID);

             return View(user);
        }


        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult EditOrderStatus(OrderStatus orderStatus)
        {

            if (ModelState.IsValid)
            {
                repository.SaveOrderStatus(orderStatus);
                TempData["message"] = string.Format("Статус {0} було збережено!", orderStatus.name);

                return RedirectToAction("OrderStatusesList");
            }
            else
            {
                return View(orderStatus);
            }
        }
        [Authorize(Roles = "Директор")]
        public ViewResult CreateOrderStatus()
        {
            
            return View("EditOrderStatus", new OrderStatus());
        }
        [Authorize(Roles = "Директор")]
        [HttpPost]
        public ActionResult DeleteOrderStatus(int statusID)
        {
            var deleted = repository.DeleteOrderStatus(statusID);
            if (deleted != null)
                TempData["message"] = string.Format("Статус {0} було видалено!", deleted.statusID);
            return RedirectToAction("OrderStatusesList");
        }


        #endregion
        //+ D,M
        #region ProductToBuy
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult ProductsToBuyList()
        {
            return View(repository.ProductsToBuy);
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult EditProductToBuy(int productToBuyID)
        {
            var user = repository.ProductsToBuy.FirstOrDefault(p => p.productToBuyID == productToBuyID);
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                        select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };

            return View(user);
        }


        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult EditProductToBuy(ProductToBuy productToBuy)
        {

            if (ModelState.IsValid)
            {
                repository.SaveProductToBuy(productToBuy);
                TempData["message"] = string.Format("Передзамовлення {0} було збережено!", productToBuy.title);

                return RedirectToAction("ProductsToBuyList");
            }
            else
            {
                return View(productToBuy);
            }
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult CreateProductToBuy()
        {
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                        select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };

            return View("EditProductToBuy", new ProductToBuy());
        }
        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult DeleteProductToBuy(int productToBuyID)
        {
            var deleted = repository.DeleteProductToBuy(productToBuyID);
            if (deleted != null)
                TempData["message"] = string.Format("Передзамовлення {0} було видалено!", deleted.title);
            return RedirectToAction("ProductsToBuyList");
        }


        #endregion
        //+ D,M
        #region ReservedProduct
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult ReservedProductsList()
        {
            return View(repository.ReservedProducts);
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult EditReservedProduct(int reservID)
        {
            var user = repository.ReservedProducts.FirstOrDefault(p => p.reservID == reservID);
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllProducts"] = from item in repository.Products.AsEnumerable()
                                      select new SelectListItem { Text = item.title, Value = item.productID.ToString() };
            ViewData["AllOrders"] = from item in repository.Orders.AsEnumerable()
                                    select new SelectListItem { Text = item.orderID.ToString(), Value = item.orderID.ToString() };
         
            return View(user);
        }


        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult EditReservedProduct(ReservedProduct reservedProduct)
        {

            if (ModelState.IsValid)
            {
                repository.SaveReservedProduct(reservedProduct);
                TempData["message"] = string.Format("Зарезервований товар {0} було збережено!", reservedProduct.reservID);

                return RedirectToAction("ReservedProductsList");
            }
            else
            {
                return View(reservedProduct);
            }
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult CreateReservedProduct()
        {
            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllProducts"] = from item in repository.Products.AsEnumerable()
                                      select new SelectListItem { Text = item.title, Value = item.productID.ToString() };
            ViewData["AllOrders"] = from item in repository.Orders.AsEnumerable()
                                    select new SelectListItem { Text = item.orderID.ToString(), Value = item.orderID.ToString() };
         
            return View("EditReservedProduct", new ReservedProduct());
        }

        [HttpPost]
        public ActionResult DeleteReservedProduct(int reservID)
        {
            var deleted = repository.DeleteReservedProduct(reservID);
            if (deleted != null)
                TempData["message"] = string.Format("Зарезервований товар {0} було видалено!", deleted.reservID);
            return RedirectToAction("ReservedProductsList");
        }


        #endregion
        //+ M
        #region For manager Product
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult ManagerProductsList()
        {

            return View(repository.Products.Where(x => x.approved == false).AsEnumerable());
        }
        [Authorize(Roles = "Менеджер, Директор")]
        public ViewResult EditManagerProduct(int productID)
        {
            var user = repository.Products.FirstOrDefault(p => p.productID == productID);


            ViewData["AllUsers"] = from item in repository.Users.AsEnumerable()
                                   select new SelectListItem { Text = item.contactPerson, Value = item.userID.ToString() };
            ViewData["AllCategories"] = from item in repository.ProductCategories.AsEnumerable()
                                        select new SelectListItem { Text = item.name, Value = item.productCategoryID.ToString() };

            return View(user);
        }


        [Authorize(Roles = "Менеджер, Директор")]
        [HttpPost]
        public ActionResult EditManagerProduct(Product product)
        {

            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Користувача {0} було збережено!", product.title);

                return RedirectToAction("ManagerProductsList");
            }
            else
            {
                return View(product);
            }
        }


        #endregion

        public ViewResult ManagerStatisticList()
        {
            return View();
        }

        public ViewResult SaleStatisticList()
        {
            return View();
        }

        public ViewResult ShoppingStatisticList()
        {
            return View();
        }

        public ViewResult VirtualStorageList()
        {
            return View();
        }
    }

}