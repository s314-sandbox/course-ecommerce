using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class AccountEditModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Не указан телефон")]
        public string Phone { get; set; }

        [Display(Name = "E-mail")]
        public string Mail { get; set; }
    }
}
