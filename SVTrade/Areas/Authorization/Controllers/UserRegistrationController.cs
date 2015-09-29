using SVTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SVTrade.Areas.Authorization.Controllers
{
    public class UserRegistrationController : Controller
    {
        public ActionResult Register()
        {
            
            return View();
        }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Register(SVTrade.Models.User U)
            {
                if (ModelState.IsValid)
                {
                    using (SVTrade.Models.TradeDBEntities dc = new SVTrade.Models.TradeDBEntities())
                    { 
                         
                        dc.User.Add(U);
                        dc.SaveChanges();
                        ModelState.Clear();
                        U = null;
                        ViewBag.Message = "Successfully Registration Done";
                    }
                }
                return View(U);
            }
        
    }
}