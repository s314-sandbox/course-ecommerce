using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace evcommerce.Models
{
    public class RegisterModel
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

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
