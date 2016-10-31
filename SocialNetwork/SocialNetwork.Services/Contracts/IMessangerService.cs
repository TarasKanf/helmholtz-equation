using System;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Contracts
{
    public interface IMessangerService
    {
        void Send(Message message);

        void Send(Guid senderId, Guid receiverId, string data);

        void Send(string senderEmail, string receiverEmail, string data);        
    }
}
