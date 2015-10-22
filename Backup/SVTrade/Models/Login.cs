using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SVTrade.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username required.", AllowEmptyStrings = false)]
        public string email { get; set; }

        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string password { get; set; }
        public bool RememberMe { get; set; }
    }
}