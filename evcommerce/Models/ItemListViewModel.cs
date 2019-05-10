using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class ItemListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Артикул")]
        public string VendorCode { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Подкатегория")]
        public string SubCategory { get; set; }

        [Display(Name = "Поставщик")]
        public string Provider { get; set; }

        [Display(Name = "Цена")]
        public int Cost { get; set; }
    }
}
