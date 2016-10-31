using System;
using System.Collections.Generic;
using System.Web.Http;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Web.WebApi.Models;
using SocialNetwork.Domain.DataTransferObjects;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class UserSearchingController : ApiController
    {
        private readonly IUserSearchingService userSearchService;

        public UserSearchingController(IUserSearchingService userSearchService) {
            this.userSearchService = userSearchService;
        }
             
        [HttpGet]
        public UserModel GetById(Guid id)
        {
            return new UserModel(userSearchService.SearchById(id));
        }

        [HttpGet]
        public UserModel GetByEmail(string email)
        {
            return new UserModel(userSearchService.SearchByEmail(email));
        }

        [HttpGet]
        public UserModel SearchByLoginOrEmail(string emailOrLogin)
        {
            return new UserModel(userSearchService.SearchByLoginOrEmail(emailOrLogin));
        }
        
        [HttpGet]
        public List<UserModel> GetByName(string firstName, string lastName) 
        {
           var users =  userSearchService.SearchByName(firstName, lastName);
           List<UserModel> usersModel = new List<UserModel>();
           foreach (var user in users)
            {
                usersModel.Add(new UserModel(user));
            }
            return usersModel;
        }

        [HttpGet]
        public List<UserModel> GetByFirstName(string firstName)
        {
            var users =  userSearchService.SearchByFirstName(firstName);
            List<UserModel> usersModel = new List<UserModel>();
            foreach (var user in users)
            {
                usersModel.Add(new UserModel(user));
            }
            return usersModel;
        }

        [HttpGet]
        public List<UserModel> GetByLastName(string lastName)
        {
            var users =  userSearchService.SearchByLastName(lastName);
            List<UserModel> usersModel = new List<UserModel>();
            foreach (var user in users)
            {
                usersModel.Add(new UserModel(user));
            }
            return usersModel;
        }
    }
}
