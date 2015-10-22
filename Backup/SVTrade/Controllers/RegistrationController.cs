using System.Linq;
using System.Web.Mvc;
using SVTrade.Abstract;
using SVTrade.Models;
using System.Collections.Generic;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;

namespace SVTrade.Controllers
{
    public class UserRegistrationController : Controller
    {
        private IRepository repository;


        public UserRegistrationController(IRepository repo)
        {
            repository = repo;
        }






        [HttpGet]
        public ActionResult Register()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (ProductCategory city in repository.ProductCategories)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = city.name,
                    Value = city.productCategoryID.ToString(),

                };
                listSelectListItems.Add(selectList);
            }

            Register citiesViewModel = new Register()
            {
                Cities = listSelectListItems
            };

            return PartialView(citiesViewModel);
        }




        [HttpPost]
        public string Register(User user, IEnumerable<string> selectedCities)
        {
            StringBuilder sb = new StringBuilder();
            if (selectedCities == null)
            {
                sb.Append("No cities are selected");
            }
            else
            {

                sb.Append(string.Join("", selectedCities));

                if (ModelState.IsValid)
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encrpass = crypto.Compute(user.password);                     
                    user.password = encrpass; 
                    User user2 = null;
                    user2 = repository.Users.Where(u => u.email == user.email || u.password == user.password || u.companyName == user.companyName).FirstOrDefault();
                    if (user2 == null)
                    {
                        user.passwordSalt = crypto.Salt;
                        user.merchantLicense = false;
                        user.tradeLicense = false;
                        user.userGroupID = 3;
                        repository.SaveUser(user);
                        foreach (string item in selectedCities)
                        {
                            ChoosedCategory ck = new ChoosedCategory();
                            ck.userID = user.userID;
                            ck.productCategoryID = Convert.ToInt32(item);
                            repository.SaveChoosedCategory(ck);
                        }



                        ModelState.Clear();
                        ViewBag.Message = "Реєстрація прошла успішно!";


                    }
                    else
                    {
                        ModelState.AddModelError(" ", "Користувач уже існує!");
                        ViewBag.Message = "Користувач уже існує!";
                    }

                }
                else
                {
                    ViewBag.Message = "Перевірте поля!";


                }



            }
            RedirectToAction("Index");
            return sb.ToString();
        }



       
    }
}