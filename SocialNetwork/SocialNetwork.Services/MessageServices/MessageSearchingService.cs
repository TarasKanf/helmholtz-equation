using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Services
{
    public class MessageSearchingService : IMessageSearchingService
    {
        private readonly IUnitOfWork work;

        public MessageSearchingService()
        {
            work = new UnitOfWork();
        }

        public MessageSearchingService(IUnitOfWork work)
        {
            this.work = work;
        }

        /// <summary>
        ///     Search message by Id.
        /// </summary>
        /// <param name="id">Id of message to find.</param>
        /// <returns>Message with given Id.</returns>
        public Message SearchById(Guid id)
        {
            return work.Messages.GetAll().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///     Search all messages sended by user with given Id.
        /// </summary>
        /// <param name="id">Sender Id.</param>
        /// <returns>All messages from sender.</returns>
        public IEnumerable<Message> SearchBySender(Guid id)
        {
            return work.Messages.GetAll().Where(x => x.SenderId == id);
        }

        /// <summary>
        ///     Search all messages received by user with given Id.
        /// </summary>
        /// <param name="id">Receiver Id.</param>
        /// <returns>All messages to receiver.</returns>
        public IEnumerable<Message> SearchByReceiver(Guid id)
        {
            return work.Messages.GetAll().Where(x => x.ReceiverId == id);
        }

        public IEnumerable<Message> SearchByUser(Guid id)
        {
            return work.Messages.GetAll().Where(x => x.ReceiverId == id || x.SenderId == id);
        }

        /// <summary>
        ///     Search messages by text.
        /// </summary>
        /// <param name="text">Text that has to be in searched messages.</param>
        /// <returns>All messages which contain given text.</returns>
        public IEnumerable<Message> SearchByText(string text)
        {
            return work.Messages.GetAll().Where(x => x.Data.Contains(text));
        }
    }
}