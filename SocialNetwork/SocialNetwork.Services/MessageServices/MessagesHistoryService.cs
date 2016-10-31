using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Services
{
    public class MessagesHistoryService : IMessageHistoryService
    {
        public List<Message> GetAllMessageHistory(Guid userId, Guid userFriendId)
        {
            var work = new UnitOfWork();
            return work.Messages.GetAll()
                .Where(c => (c.SenderId == userId && c.ReceiverId == userFriendId)
                            || (c.ReceiverId == userId && c.SenderId == userFriendId))
                .OrderBy(c => c.Date).ToList();
        }
    }
}