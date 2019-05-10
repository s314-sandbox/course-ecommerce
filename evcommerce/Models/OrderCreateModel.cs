using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class OrderCreateModel
    {
        [Display(Name = "Способ оплаты")]
        [Required(ErrorMessage = "Не определен вид платежа")]
        public int PaymentId { get; set; }

        [Display(Name = "Способ доставки")]
        [Required(ErrorMessage = "Не выбран способ доставки")]
        public int DeliveryId { get; set; }

        [Display(Name = "Адрес доставки")]
        [Required(ErrorMessage = "Не выбран адрес доставки")]
        public int AdressId { get; set; }

        public List<BasketPositionView> BasketPositionView { get; set; }
    }
}
