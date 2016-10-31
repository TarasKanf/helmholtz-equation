using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Web.WebApiWrapper.Properties;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class FriendsService : BaseService
    {
        public Response<List<UserModel>> GetFriends(Guid id)
        {
            return GetDataFromRoute<List<UserModel>>(
                Host + string.Format(Resources.RouteFriendsById, id));
        }

        public Response<List<UserModel>> GetMutualFriends(Guid user1, Guid user2)
        {
            return GetDataFromRoute<List<UserModel>>(
                Host + string.Format(Resources.RouteMutualFriends, user1, user2));
        }

        public Response<List<UserModel>> GetMutualFriends(Guid user1, string user2Email)
        {
            return GetDataFromRoute<List<UserModel>>(
                Host + string.Format(Resources.RouteMutualFriendsWithEmail, user1, user2Email));
        }

        /// <summary>
        ///     Makes user1 and user2 friends.
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <returns>True if http status code of response was successful</returns>
        public Task<Response> AddFriends(Guid user1, Guid user2)
        {
            var model = new FriendsModel
            {
                UserId1 = user1,
                UserId2 = user2
            };
            return PutDataToRoute(Host + Resources.RouteAddFriend,model);
        }
    }
}