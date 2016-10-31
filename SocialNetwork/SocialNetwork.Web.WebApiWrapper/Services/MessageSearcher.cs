using System;
using System.Collections.Generic;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Web.WebApiWrapper.Properties;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class MessageSearcher: BaseService
    {
        public Response<MessageWithGuidModel> GetById(Guid id)
        {
            return GetDataFromRoute<MessageWithGuidModel>(
                Host + string.Format(Resources.RouteSearchMessageById, id));
        }

        public Response<List<MessageWithGuidModel>> GetBySender(Guid id)
        {
            return GetDataFromRoute<List<MessageWithGuidModel>>(
                Host + string.Format(Resources.RouteSearchMessageBySenderId, id));
        }

        public Response<List<MessageWithGuidModel>> GetByReceiver(Guid id)
        {
            return GetDataFromRoute<List<MessageWithGuidModel>>(
                Host + string.Format(Resources.RouteSearchMessageByReceiverId, id));
        }

        public Response<MessageWithGuidModel> GetByUserId(Guid id)
        {
            return GetDataFromRoute<MessageWithGuidModel>(
                Host + string.Format(Resources.RouteSearchMessageByUserId, id));
        }

        public Response<IEnumerable<MessageWithGuidModel>> GetByText(string text)
        {
            return GetDataFromRoute<IEnumerable<MessageWithGuidModel>>(
                Host + string.Format(Resources.RouteSearchMessageByText, text));
        }

    }
}
