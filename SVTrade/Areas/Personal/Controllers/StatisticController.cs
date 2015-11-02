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
    public class StatisticController : Controller
    {
        private TradeDBEntities db = new TradeDBEntities();
        private IRepository r;

        public StatisticController(IRepository repo)
        {
            r = repo;
        }
    
        // GET: Personal/Statistic
        public ActionResult Index()
        {
            return View();
        }
    }
}