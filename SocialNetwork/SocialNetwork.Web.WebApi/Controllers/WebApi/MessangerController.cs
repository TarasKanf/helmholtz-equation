using System.Web.Http;
using SocialNetwork.Services;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Web.WebApi.Models;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class MessangerController : ApiController
    {
        private readonly IMessangerService messanger;

        public MessangerController(IMessangerService service)
        {
            messanger = service;
        }

        [HttpPut]
        [Route("api/messanger/guid")]
        public void Send([FromBody] MessageWithGuidModel message)
        {
            messanger.Send(message.SenderId, message.ReceiverId, message.Data);
        }

        [HttpPut]
        [Route("api/messanger/email")]
        public void Send([FromBody] MessageWithEmailModel message)
        {
            messanger.Send(message.SenderEmail, message.ReceiverEmail, message.Data);
        }
    }
}
