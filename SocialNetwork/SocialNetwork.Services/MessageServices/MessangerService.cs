using System;
using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Services
{
    public class MessangerService : IMessangerService
    {
        private readonly IUnitOfWork work;

        public MessangerService()
        {
            work = new UnitOfWork();
        }

        public MessangerService(IUnitOfWork unitOfWork)
        {
            work = unitOfWork;
        }

        public void Send(Message message)
        {
            work.Messages.Create(message);
            var connection =
                work.Connections.GetAll()
                    .FirstOrDefault(x => ((x.User1Id == message.SenderId) && (x.User2Id == message.ReceiverId))
                                         || ((x.User1Id == message.ReceiverId) && (x.User2Id == message.SenderId)));

            if (connection == null && message.SenderId != message.ReceiverId)
            {
                work.Connections.Create(new Connection(message.SenderId, message.ReceiverId));
            }

            work.Save();

            // TODO may be mistake with messageId
            NotifyAboutNewMessage(message);
        }

        public void Send(Guid senderId, Guid receiverId, string data)
        {
            var message = new Message(senderId, receiverId, data);
            Send(message);
        }

        public void Send(string senderEmail, string receiverEmail, string data)
        {
            var senderId = work.Users.GetAll().First(x => x.Email == senderEmail).Id;
            var receiverId = work.Users.GetAll().First(x => x.Email == receiverEmail).Id;
            var message = new Message(senderId, receiverId, data);
            Send(message);
        }

        private void NotifyAboutNewMessage(Message message)
        {
            AuthenticationService.SessionManager.NotifySessionAboutMessage(message);
        }
    }
}