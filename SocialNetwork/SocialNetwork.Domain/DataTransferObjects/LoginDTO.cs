using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.DataTransferObjects
{
    public class LoginDTO
    {
        public LoginDTO()
        {
        }

        public LoginDTO(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [Required(ErrorMessage = @"Необхідно ввести логін")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = @"Необхідно ввести пароль")]
        [MinLength(6, ErrorMessage = @"Мінімальна довжина паролю 6 символів")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }       
    }
}