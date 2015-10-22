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
            return PartialView();
        }


        [HttpPost]
        public ActionResult Register(User user, Register reg)
        {
            string a = "";
            string b = "";
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrpass = crypto.Compute(reg.password);
                user.password = encrpass;
                User user2 = null;
                user2 = repository.Users.Where(u => u.email == reg.email).FirstOrDefault();
                if (user2 == null)
                {
                    user.passwordSalt = crypto.Salt;
                    user.merchantLicense = false;
                    user.tradeLicense = false;
                    user.userGroupID = 1;
                    user.email = reg.email;
                    user.companyName = reg.companyName;
                    user.contactPerson = reg.contactPerson;
                    user.address = reg.address;
                    user.juridicalAddress = reg.juridicalAddress;
                    user.post = reg.post;
                    user.phoneNumber = reg.phoneNumber;
                    user.approved = false;
                    repository.SaveUser(user);
                    ModelState.Clear();
                    a = "Login";
                    b = "Account";
                    RedirectToAction(a, b);
                }
                else
                {
                    ViewBag.Message = "Користувач уже існує!";
                }

            }
            else
            {
                ViewBag.Message = "Невірні дані!";
            }
            return View(); ;
        }
    }
}