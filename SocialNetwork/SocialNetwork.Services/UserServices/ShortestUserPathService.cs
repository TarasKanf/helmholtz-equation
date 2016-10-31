using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Services
{
    public class ShortestUserPathService : IShortestUserPathService
    {
        private UnitOfWork work;

        /// <summary>
        ///     Get the shortest path of users between two users
        /// </summary>
        /// <param name="user1">First user</param>
        /// <param name="user2">Second user</param>
        /// <returns>Enumeration of users from shortest path between two users</returns>
        public IEnumerable<User> GetShortestPath(Guid user1, Guid user2)
        {
            work = new UnitOfWork();
            var shortestPathId = AnalyzerBridge.GetShortestPath(
                work.Connections.GetAll().ToArray(),
                user1,
                user2);
            return shortestPathId.Reverse()
                     .Select(id => work.Users
                     .GetAll().FirstOrDefault(x => x.Id == id))
                     .ToList();
        }

        public IEnumerable<User> GetShortestPath(Guid user1, string user2Email)
        {
            UserSearchingService searchingService = new UserSearchingService();
            var user = searchingService.SearchByEmail(user2Email);
            return GetShortestPath(user1, user.Id);
        }
    }
}