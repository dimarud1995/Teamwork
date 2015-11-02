using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SVTrade.Models
{
    public class SMS
    {
        [DisplayName("Номер, кому призначається СМС(Приклад: 380993334455)")]
        [Required(ErrorMessage = "Введіть номер телефону", AllowEmptyStrings = false)]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неправильний формат номера телефону")]

        public string Numbers { get; set; }

        [DisplayName("Від кого відправляється СМС(Приклад: SVTRADE)")]
        public string SenderId { get; set; }

        [DisplayName("Текст повідомлення")]
        public string Message { get; set; }
    }
}