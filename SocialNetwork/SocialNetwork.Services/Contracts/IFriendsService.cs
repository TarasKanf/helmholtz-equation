using System;
using System.Collections.Generic;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Contracts
{
   public interface IFriendsService
    {
         List<User> GetFriends(Guid id);

         IEnumerable<User> GetMutualFriends(Guid user1, Guid user2); 

         IEnumerable<User> GetMutualFriends(Guid user1, string user2Email);

         void AddToFriends(Guid userId1, Guid userId2);
    }
}
