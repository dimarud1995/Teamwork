using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVTrade.Models
{
    public class SMS
    {
        public string Numbers { get; set; }

        public string SenderId { get; set; }

        public string Message { get; set; }
    }
}