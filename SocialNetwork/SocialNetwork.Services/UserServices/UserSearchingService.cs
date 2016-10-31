using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Services
{
    public class UserSearchingService : IUserSearchingService
    {
        private readonly IUnitOfWork work;

        public UserSearchingService()
        {
            work = new UnitOfWork();
        }

        public UserSearchingService(IUnitOfWork work)
        {
            this.work = work;
        }
        
        /// <summary>
        /// Get user with this id
        /// </summary>
        /// <param name="id">identifier of user</param>
        /// <returns>user or null if not found</returns>
        public User SearchById(Guid id)
        {
            return work.Users.GetAll().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">user's email</param>
        /// <returns>User or null if not found</returns>
        public User SearchByEmail(string email)
        {
            return work.Users.GetAll().FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="login">user's login</param>
        /// <returns>User or null if not found</returns>
        public User SearchByLogin(string login)
        {
            return work.Users.GetAll().FirstOrDefault(x => x.UserName == login);
        }

        /// <summary>
        /// Get user by login or e-mail
        /// </summary>
        /// <param name="data">user's login or e-mail</param>
        /// <returns>User or null if not found</returns>
        public User SearchByLoginOrEmail(string data)
        {
            User user = SearchByLogin(data);
            return (user != null) ? user : SearchByEmail(data);
        }

        /// <summary>
        /// Get user by its first name and last name
        /// </summary>
        /// <param name="firstName">user's first name</param>
        /// <param name="lastName">user's last name</param>
        /// <returns>enumeration of users or null, if not found</returns>
        public IEnumerable<User> SearchByName(string firstName, string lastName)
        {
            return work.Users.GetAll().Where(x => x.FirstName == firstName && x.LastName == lastName);
        }

        /// <summary>
        /// Get users with that first name
        /// </summary>
        /// <param name="firstName">first name of user</param>
        /// <returns>enumeration of users or null</returns>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            return work.Users.GetAll().Where(x => x.FirstName == firstName);
        }

        /// <summary>
        /// Get users which have that last name
        /// </summary>
        /// <param name="lastName">last name of user</param>
        /// <returns>enumeration of users or null</returns>
        public IEnumerable<User> SearchByLastName(string lastName)
        {
            return work.Users.GetAll().Where(x => x.LastName == lastName);
        }
    }
}