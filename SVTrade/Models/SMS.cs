using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SVTrade.Models
{
    public class SMS
    {
        [DisplayName("Номер, кому призначається СМС (Приклад: 380993334455)")]
        [Required(ErrorMessage = "Введіть номер телефону", AllowEmptyStrings = false)]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неправильний формат номера телефону")]
        public string Numbers { get; set; }

        [DisplayName("Відправник СМС (Приклад: SVTRADE)")]
        [Required(ErrorMessage = "Введіть відправника", AllowEmptyStrings = false)]
        public string SenderId { get; set; }

        [DisplayName("Текст повідомлення")]
        [Required(ErrorMessage = "Введіть текст", AllowEmptyStrings = false)]
        public string Message { get; set; }
    }
}