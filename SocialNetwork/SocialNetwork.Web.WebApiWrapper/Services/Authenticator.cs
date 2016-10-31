using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SocialNetwork.Web.WebApiWrapper.Properties;
using SocialNetwork.Domain.DataTransferObjects;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class Authenticator : BaseService
    {
        /// <summary>
        /// Return status code of Http request
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public string Register(RegistrationDTO registerModel)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(Host + Resources.RouteRegister, registerModel).Result;
                return response.StatusCode.ToString();
            }
        }

        /// <summary>
        /// Gets a token after registration
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("Password", password)
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(Host + "/Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                AccessToken = tokenDictionary["access_token"];

                return tokenDictionary;
            }
        }

        public Response<SessionInfo> LogIn(LoginDTO logInModel)
        {
            return PostDataToRoute<SessionInfo, LoginDTO>(Host + Resources.RouteLogIn, logInModel);
        }

        public Task<Response> LogOut(string sessionKey)
        {
            return PutDataToRoute(Host + Resources.RouteLogOut, sessionKey);
        }
    }
}
