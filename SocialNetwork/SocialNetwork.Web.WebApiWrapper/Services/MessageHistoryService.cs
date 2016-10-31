using System;
using System.Collections.Generic;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Web.WebApiWrapper.Properties;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class MessageHistoryService : BaseService
    {
        public Response<List<MessageWithGuidModel>> GetAllMessageHistory(Guid userId, Guid userFriendId)
        {
            return GetDataFromRoute<List<MessageWithGuidModel>>(
                Host + string.Format(Resources.RouteMessageHistory, userId, userFriendId));
        }
    }
}
