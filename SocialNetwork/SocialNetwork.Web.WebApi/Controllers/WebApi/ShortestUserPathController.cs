using System;
using System.Collections.Generic;
using System.Web.Http;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class ShortestUserPathController : ApiController
    {
        private readonly IShortestUserPathService shortestUserPathService;

        public ShortestUserPathController(IShortestUserPathService shortestUserPathService)
        {
            this.shortestUserPathService = shortestUserPathService;
        }

        [HttpGet]
        public List<UserModel> GetShortestPath(Guid user1, Guid user2)
        {
            var users = shortestUserPathService.GetShortestPath(user1, user2);
            var usersModel = new List<UserModel>();
            foreach (var user in users)
            {
                usersModel.Add(new UserModel(user));
            }

            return usersModel;
        }

        [HttpGet]
        public List<UserModel> GetShortestPath(Guid user1, string user2Email)
        {
            var users = shortestUserPathService.GetShortestPath(user1, user2Email);
            var usersModel = new List<UserModel>();
            foreach (var user in users)
            {
                usersModel.Add(new UserModel(user));
            }

            return usersModel;
        }
    }
}