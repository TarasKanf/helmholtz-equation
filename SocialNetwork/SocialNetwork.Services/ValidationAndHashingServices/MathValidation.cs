using System.Text.RegularExpressions;

namespace SocialNetwork.Services.ValidationAndHashingServices
{
    public class MathValidation
    {
        public MathValidation()
        {
            Pattern = @"math\(\b([\d,*-/^+%]*)\b\)";
        }

        public string Pattern { get; }

        /// <summary>
        ///     Check out whether include at least one math(...) in message
        /// </summary>
        /// <param name="message">will check someone's message</param>
        /// <returns>true if founded at least one expression math(..), false if not</returns>
        public bool ValidMath(string message)
        {
            if (message == string.Empty)
            {
                return false;
            }

            if (Regex.IsMatch(message, Pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}