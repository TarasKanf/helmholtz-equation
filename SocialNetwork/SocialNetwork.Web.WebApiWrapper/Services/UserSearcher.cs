using System;
using System.Collections.Generic;
using System.Net.Http;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Web.WebApiWrapper.Properties;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class UserSearcher : BaseService
    {
        public Response<UserModel> GetById(Guid id)
        {
            return GetDataFromRoute<UserModel>(
                Host 
                + string.Format(Resources.RouteSearchUserById, id));
        }
        public Response<UserModel> GetByLoginOrEmail(string emailOrLogin)
        {
            return GetDataFromRoute<UserModel>(
                Host + string.Format(Resources.RouteSearchUserByLoginOrEmail, emailOrLogin));
        }
        

        public Response<UserModel> GetByEmail(string email)
        {
            return GetDataFromRoute<UserModel>(
                Host + string.Format(Resources.RouteSearchUserByEmail, email));
        }

        public Response<List<UserModel>> GetByName(string firstName, string lastName)
        {
            return
                GetDataFromRoute<List<UserModel>>(
                    Host + string.Format(Resources.RouteSearchUserByFLName, firstName, lastName));
        }

        public Response<List<UserModel>> GetByFirstName(string firstName)
        {
            return GetDataFromRoute<List<UserModel>>(
                Host + string.Format(Resources.RouteSearchUserByFName, firstName));
        }

        public Response<List<UserModel>> GetByLastName(string lastName)
        {
            return GetDataFromRoute<List<UserModel>>(
                Host + string.Format(Resources.RouteSearchUserByFName, lastName));
        }
    }
}