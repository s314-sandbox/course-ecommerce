using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class AdressCreateModel
    {
        [Required(ErrorMessage = "Не определена страна")]
        public int Country { get; set; }

        [Required(ErrorMessage = "Не указан город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Не указана улица")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Не указан номер дома")]
        public int House { get; set; }

        [Required(ErrorMessage = "Не указан номер квартиры")]
        public string Flat { get; set; }

        public string Info { get; set; }
    }
}
