using System;
using System.Collections.Generic;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Contracts
{
    public interface IMessageSearchingService
    {
        Message SearchById(Guid id);

        IEnumerable<Message> SearchBySender(Guid id);

        IEnumerable<Message> SearchByReceiver(Guid id);

        IEnumerable<Message> SearchByUser(Guid id);

        IEnumerable<Message> SearchByText(string text);
    }
}
