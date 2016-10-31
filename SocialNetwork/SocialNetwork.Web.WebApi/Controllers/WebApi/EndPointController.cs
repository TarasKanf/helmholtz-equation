using System.Web.Http;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Web.WebApi.Controllers.WebApi
{
    public class EndPointController : ApiController
    {
        private object loker = new object();
        private readonly IEndPointSetter endPointSetter;

        public EndPointController(IEndPointSetter setter)
        {
            endPointSetter = setter;
        }

        [HttpPut]
        [Route("api/endpoint")]
        public void Send([FromBody] ClientEndPoint endPoint)
        {
             endPointSetter.SetEndPoint(endPoint.SessionKey, endPoint.EndPoint);
        }
    }
}