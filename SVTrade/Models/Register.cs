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

        [Required(ErrorMessage = "Ведіть пароль", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [System.Web.Mvc.CompareAttribute("password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }



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
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неправильний формат номера телефону")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Введіть email", AllowEmptyStrings = false)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Неправильний формат пошти")]
        public string email { get; set; }

    }
}