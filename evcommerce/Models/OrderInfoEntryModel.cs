using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class OrderInfoEntryModel
    {
        [Display(Name = "ID Заказа")]
        public int Id { get; set; }

        [Display(Name = "Дата заказа")]
        public DateTime Date { get; set; }

        [Display(Name = "Способ оплаты")]
        public string Payment { get; set; }

        [Display(Name = "Способ доставки")]
        public string Delivery { get; set; }

        [Display(Name = "Адрес доставки")]
        public string Address { get; set; }

        public List<OrderedItemModel> OrderedItemModel { get; set; }
    }
}
