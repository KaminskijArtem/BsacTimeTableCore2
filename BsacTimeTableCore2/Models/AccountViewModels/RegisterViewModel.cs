using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Эл. адрес")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен быть минимум {2} и максимум {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли на совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}
