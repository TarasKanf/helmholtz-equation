using SocialNetwork.Services.ValidationAndHashingServices;

namespace SocialNetwork.Services
{
    public class UserValidationService
    {
        private EmailValidation emailValidator;
        private PasswordValidation passwordValidator;     

        public UserValidationService()
        {
            emailValidator = new EmailValidation();
            passwordValidator = new PasswordValidation();
        }        

        /// <summary>
        ///     Check if given string has an email format.
        /// </summary>
        /// <param name="email">String to check.</param>
        /// <returns>If given string has a right format.</returns>
        public bool ValidEmail(string email)
        {
            return emailValidator.ValidEmail(email);
        }

        /// <summary>
        ///     Check weather password is long enough.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidPassword(string password)
        {
            return passwordValidator.ValidPassword(password);
        }
    }
}