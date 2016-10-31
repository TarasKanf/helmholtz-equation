using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Threading.Tasks;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class BaseService
    {
        public static string Host { get; } = ConfigurationManager.AppSettings["webApiHost"];

        /// <summary>
        /// Stores access token. Is set by authentication service.
        /// </summary>
        protected static string AccessToken { get; set; } = string.Empty;

        /// <summary>
        ///     Creates authorized client if token is not empty or null.
        /// </summary>
        /// <returns></returns>
        protected static HttpClient CreateClient()
        {
            var client = new HttpClient();

            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
            }

            return client;
        }

        protected Response<T> GetDataFromRoute<T>(string route)
        {
            var response = new Response<T>();
            using (var client = CreateClient())
            {
                var message = client.GetAsync(route).Result;

                response.IsSuccessful = message.IsSuccessStatusCode;
                if (response.IsSuccessful)
                {
                    response.ResultTask = message.Content.ReadAsAsync<T>();
                }
            }

            return response;
        }

        protected Response<TReturn> PostDataToRoute<TReturn,TContent>(string route, TContent content)
        {
            var response = new Response<TReturn>();
            using (var client = CreateClient())
            {
                var message = client.PostAsJsonAsync(route,content).Result;

                response.IsSuccessful = message.IsSuccessStatusCode;
                if (response.IsSuccessful)
                {
                    response.ResultTask = message.Content.ReadAsAsync<TReturn>();
                }
            }

            return response;
        }

        protected async Task<Response> PutDataToRoute<TContent>(string route, TContent content)
        {
            var response = new Response();
            using (var client = CreateClient())
            {
                var message = await client.PutAsJsonAsync(route, content);

                response.IsSuccessful = message.IsSuccessStatusCode;
            }

            return response;
        }
    }
}
