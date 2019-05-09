using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class AdminOrderListEntryModel
    {
        [Display(Name = "ID Заказа")]
        public int Id { get; set; }

        [Display(Name = "Имя заказчика")]
        public string UserName { get; set; }

        [Display(Name = "Фамилия заказчика")]
        public string UserSurname { get; set; }

        [Display(Name = "Телефон")]
        public string UserPhone { get; set; }

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
