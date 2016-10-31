using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.Services
{
    public class Md5CryptoService
    {
        /// <summary>
        ///     Hashed given string with MD5 algorithm.
        /// </summary>
        /// <param name="input">String to hash.</param>
        /// <returns>Hashed string</returns>
        public string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var hashedPass = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                hashedPass.Append(hash[i].ToString());
            }

            return hashedPass.ToString();
        }
    }
}