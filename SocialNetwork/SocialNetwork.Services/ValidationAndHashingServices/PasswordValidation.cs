namespace SocialNetwork.Services.ValidationAndHashingServices
{
    public class PasswordValidation
    {
        public const int MinPasswordLength = 6;

        /// <summary>
        ///     Check weather password is long enough.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidPassword(string password)
        {
            if (password.Length >= MinPasswordLength)
            {
                return true;
            }

            return false;
        }
    }
}
