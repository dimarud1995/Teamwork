using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace SVTrade.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Ведіть пароль", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль має бути більше 8 символів")]
        public string password { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [System.Web.Mvc.CompareAttribute("password")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<string> SelectedCities { get; set; }

        public int productCategoryID { get; set; }
        public string name { get; set; }

        public virtual List<int> Categories { get; set; }

        [Required(ErrorMessage = "Введіть назву компанії", AllowEmptyStrings = false)]
        public string companyName { get; set; }

        [Required(ErrorMessage = "Введіть юридичну адресу", AllowEmptyStrings = false)]
        public string juridicalAddress { get; set; }

        [Required(ErrorMessage = "Введіть адресу потужностей", AllowEmptyStrings = false)]
        public string address { get; set; }

        [Required(ErrorMessage = "Введіть контактну особу", AllowEmptyStrings = false)]
        public string contactPerson { get; set; }

        [Required(ErrorMessage = "Введіть посаду", AllowEmptyStrings = false)]
        public string post { get; set; }

        [Required(ErrorMessage = "Введіть номер телефону", AllowEmptyStrings = false)]
        public string phoneNumber { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
                    ErrorMessage = "Please provide valid email id")]

        public string email { get; set; }
       
    }
}