using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class OrderCreateModel
    {
        [Required(ErrorMessage = "Не определен вид платежа")]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Не выбран способ доставки")]
        public int DeliveryId { get; set; }

        [Required(ErrorMessage = "Не выбран адрес доставки")]
        public int AdressId { get; set; }

        public List<BasketPositionView> BasketPositionView { get; set; }
    }
}
