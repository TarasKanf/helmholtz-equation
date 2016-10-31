using System.Text.RegularExpressions;

namespace SocialNetwork.Services.ValidationAndHashingServices
{
    public class EmailValidation
    {
        private const string Pattern =
            @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        /// <summary>
        ///     Check weather given string has an email format.
        /// </summary>
        /// <param name="email">String to check.</param>
        /// <returns>If given string has a right format.</returns>
        public bool ValidEmail(string email)
        {
            if (email == string.Empty)
            {
                return false;
            }

            if (Regex.IsMatch(email, Pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}