using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SVTrade.Areas.MappingProducts.Models;

namespace SVTrade.Areas.MappingProducts.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public CartLine CartLine { get; set; }
        public string ReturnUrl { get; set; }
    }
}