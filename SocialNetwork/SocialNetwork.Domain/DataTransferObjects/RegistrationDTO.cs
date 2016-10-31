using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Domain.DataTransferObjects
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = @"Необхідно ввести логін")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = @"Необхідно ввести логін")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = @"Необхідно ввести пароль")]
        [MinLength(6, ErrorMessage = @"Мінімальна довжина паролю повинна складати 6 символів")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Сonfirm password")]
        [Compare("Password", ErrorMessage = @"Паролі не співпадають")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = @"Необхідно ввести ім'я")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = @"Необхідно ввести ім'я")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
    }
}