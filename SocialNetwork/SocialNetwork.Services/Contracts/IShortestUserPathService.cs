using System;
using System.Collections.Generic;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Contracts
{
    public interface IShortestUserPathService
    {
        IEnumerable<User> GetShortestPath(Guid user1, Guid user2);

        IEnumerable<User> GetShortestPath(Guid user1, string user2Email);
    }
}
