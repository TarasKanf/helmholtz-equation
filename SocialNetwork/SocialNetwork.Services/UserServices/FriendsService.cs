using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Services
{
    public class FriendsService : IFriendsService
    {
        /// <summary>
        ///     Get all friends of user
        /// </summary>
        /// <param name="id">user's id</param>
        /// <returns>Enumeration of all friends</returns>
        public List<User> GetFriends(Guid id)
        {
            List<User> friends = new List<User>();           
            using (var work = new UnitOfWork())
            {
               var connections = work.Connections.GetAll()
                                     .Where(x => x.User1Id == id || x.User2Id == id).ToList();
                friends = connections.Select(con => con.User1Id == id
                    ? work.Users.GetAll().FirstOrDefault(x => x.Id == con.User2Id)
                    : work.Users.GetAll().FirstOrDefault(x => x.Id == con.User1Id)).ToList();
            }

            return friends;
        }

        /// <summary>
        ///     Get mutual friends of two users
        /// </summary>
        /// <param name="user1">user1's id</param>
        /// <param name="user2">user2's id</param>
        /// <returns>Enumeration of mutual friends</returns>
        public IEnumerable<User> GetMutualFriends(Guid user1, Guid user2)
        {
            if (user1 == user2)
            {
                return GetFriends(user1);
            }

            using (var work = new UnitOfWork())
            {
                var mutualFriendsArr = AnalyzerBridge.GetMutualFriends(
                    work.Connections.GetAll().ToArray(),
                    user1,
                    user2);

                return mutualFriendsArr.Select(
                    friend => work.Users.GetAll()
                        .FirstOrDefault(x => x.Id == friend))
                    .ToList();
            }
        }

        /// <summary>
        ///     Get mutual friends of two users
        /// </summary>
        /// <param name="user1">first user's id</param>
        /// <param name="user2Email">second user's email</param>
        /// <returns></returns>
        public IEnumerable<User> GetMutualFriends(Guid user1, string user2Email)
        {
            var searchingService = new UserSearchingService();
            var user = searchingService.SearchByEmail(user2Email);
            var user2 = user?.Id ?? new Guid();

            return GetMutualFriends(user1, user2);
        }

        /// <summary>
        /// Adding friend to user
        /// </summary>
        /// <param name="userId1">first user of friend connections</param>
        /// <param name="userId2">second user of friend connection</param>
        public void AddToFriends(Guid userId1, Guid userId2)
        {
            using (var work = new UnitOfWork())
            {
                work.Connections.Create(
                    new Connection
                    {
                        User1Id = userId1,
                        User2Id = userId2
                    });

                work.Save();
            }
        }
    }
}