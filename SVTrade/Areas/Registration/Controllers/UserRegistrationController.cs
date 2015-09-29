using System.Linq;
using System.Web.Mvc;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Areas.Registration.Controllers
{
    public class UserRegistrationController : Controller
    {
         private IRepository repository;


         public UserRegistrationController(IRepository repo)
        {
            repository = repo;
        }
           
      

        public ViewResult Register()
        {
             return View(new User());
        }



        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                User user2 = null;
                user2 = repository.Users.Where(u => u.email == user.email || u.password == user.password || u.companyName == user.companyName ).FirstOrDefault();
                if (user2 == null)
                {
                    user.merchantLicense = false;
                    user.tradeLicense = false;
                    user.userGroupID = 1;
                    repository.SaveUser(user);
                    ModelState.Clear();
                    //ViewBag.Message = "Реєстрація прошла успішно!";
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError(" ", "Користувач уже існує!");
                    ViewBag.Message = "Користувач уже існує!";
                }
                return View(user);
            }
            else
            {
                ViewBag.Message = "Перевірте поля!";
                 
                return View(user);
            }
        }


    }
}