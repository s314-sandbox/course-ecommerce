using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class AdressCreateModel
    {
        [Display(Name = "Страна")]
        [Required(ErrorMessage = "Не определена страна")]
        public int Country { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessage = "Не указан город")]
        public string City { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Не указана улица")]
        public string Street { get; set; }

        [Display(Name = "Номер дома")]
        [Required(ErrorMessage = "Не указан номер дома")]
        public int House { get; set; }

        [Display(Name = "Квартира")]
        [Required(ErrorMessage = "Не указан номер квартиры")]
        public string Flat { get; set; }

        [Display(Name = "Примечание")]
        public string Info { get; set; }
    }
}
