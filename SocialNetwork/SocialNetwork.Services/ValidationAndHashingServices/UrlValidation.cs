using System.Text.RegularExpressions;

namespace SocialNetwork.Services.ValidationAndHashingServices
{
    public class UrlValidation
    {
        private const string Pattern = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";

        /// <summary>
        ///     Check weather given string has an url format.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool ValidUrl(string url)
        {
            if (Regex.IsMatch(url.ToLower(), Pattern))
            {
                return true;
            }

            return false;
        }
    }
}