using System;
using System.Collections.Generic;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Contracts
{
    public interface IMessageHistoryService
    {
        List<Message> GetAllMessageHistory(Guid userId, Guid userFriendId);
    }
}
