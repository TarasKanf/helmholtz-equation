using System;
using System.Collections.Generic;
using System.Web.Http;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services.Contracts;
using FriendsModel = SocialNetwork.Web.WebApi.Models.FriendsModel;

namespace SocialNetwork.Web.WebApi.Controllers 
{
    public class FriendsController : ApiController
    {
        private readonly IFriendsService friendsService;

        public FriendsController(IFriendsService ifriendsService) {
            friendsService = ifriendsService;
        }
        
        [HttpGet]
        public List<UserModel> GetFriends(Guid id)
        {
            var friends = friendsService.GetFriends(id);
            var friendsModel = new  List<UserModel>();
            foreach (var friend in friends)
            {
                friendsModel.Add(new UserModel(friend));
            }

            return friendsModel;
        }

        [HttpGet]
        public List<UserModel> GetMutualFriends(Guid user1, Guid user2)
        {
            var friends = friendsService.GetMutualFriends(user1, user2);
            var friendsModel = new List<UserModel>();
            foreach (var friend in friends)
            {
                friendsModel.Add(new UserModel(friend));
            }

            return friendsModel;
        }

        [HttpGet]
        public List<UserModel> GetMutualFriends(Guid user1, string user2Email)
        {
            var friends = friendsService.GetMutualFriends(user1, user2Email);
            var friendsModel = new List<UserModel>();
            foreach (var friend in friends)
            {
                friendsModel.Add(new UserModel(friend));
            }

            return friendsModel;
        }

        [HttpPut]
        [Route("api/Friends/add")]
        public void AddFriends([FromBody] FriendsModel friendsModel)
        {
            friendsService.AddToFriends(friendsModel.userId1, friendsModel.userId2);
        }


    }
}