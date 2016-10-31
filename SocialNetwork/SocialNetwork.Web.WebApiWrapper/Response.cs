using System.Threading.Tasks;

namespace SocialNetwork.Web.WebApiWrapper
{
    public class Response
    {
        public bool IsSuccessful { get; set; }
    }

    public class Response<T> : Response
    {
        public Task<T> ResultTask { get; set; }
    }
}