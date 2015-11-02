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
    public class StatisticController : Controller
    {
        private TradeDBEntities db = new TradeDBEntities();

        private IRepository r;

        public StatisticController(IRepository repo)
        {
            try
            {
                SVTrade.LoggedUserInfo.SetLoggedUser(Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name));
            }
            catch { }
            r = repo;
        }
        // GET: Personal/Statistic
        public ActionResult Index()
        {
            return View();
        }
    }
}