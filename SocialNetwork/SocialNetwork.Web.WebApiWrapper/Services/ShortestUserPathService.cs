using System;
using System.Collections.Generic;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Web.WebApiWrapper.Properties;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class ShortestUserPathService : BaseService
    {
        public Response<List<UserModel>> GetShortestPath(Guid user1, Guid user2)
        {
            return GetDataFromRoute<List<UserModel>>(
                Host + string.Format(Resources.RouteShortestUserPath, user1, user2));
        }

        public Response<List<UserModel>> GetShortestPath(Guid user1, string user2Email)
        {
            return GetDataFromRoute<List<UserModel>>(
                Host + string.Format(Resources.RouteShortestUserPathEmail, user1, user2Email));
        }
    }
}
