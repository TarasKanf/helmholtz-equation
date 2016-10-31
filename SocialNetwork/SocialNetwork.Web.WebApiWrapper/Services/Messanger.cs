using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Web.WebApiWrapper.Properties;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class Messanger : BaseService
    {
        /// <summary>
        /// </summary>
        /// <param name="senderEmail"></param>
        /// <param name="recieverEmail"></param>
        /// <param name="text"></param>
        /// <returns>Returns string with status code of sended request</returns>
        public async Task<Response> SendAsync(string senderEmail, string recieverEmail, string text)
        {
            var messageWithEmailModel = new MessageWithEmailModel
            {
                ReceiverEmail = recieverEmail,
                SenderEmail = senderEmail,
                Data = text
            };

            return await PutDataToRoute(
                Host + Resources.RouteSendEmailMessage,
                messageWithEmailModel);
        }

        public async Task<Response> SendAsync(Guid senderId, Guid recieverId, string text)
        {
            var messageWithGuidModel = new MessageWithGuidModel
            {
                ReceiverId = recieverId,
                SenderId = senderId,
                Data = text
            };

            return await PutDataToRoute(
                Host + Resources.RouteSendGuidMessage,
                messageWithGuidModel);
        }
    }
}