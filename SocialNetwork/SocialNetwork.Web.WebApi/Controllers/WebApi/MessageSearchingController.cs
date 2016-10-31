using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Web.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class MessageSearchingController : ApiController
    { 
        private IMessageSearchingService messageSearchingService;

        public MessageSearchingController(IMessageSearchingService messageSearchingService)
        {
            this.messageSearchingService = messageSearchingService;
        }

        [HttpGet]
        public MessageWithGuidModel SearchById(Guid id)
        {
            return new MessageWithGuidModel(messageSearchingService.SearchById(id));
        }

        [HttpGet]
        public List<MessageWithGuidModel> SearchBySender(Guid senderId)
        {
            var messages = messageSearchingService.SearchBySender(senderId);
            List<MessageWithGuidModel> messagesModel = new List<MessageWithGuidModel>();
            foreach (var msg in messages)
            {
                messagesModel.Add(new MessageWithGuidModel(msg));
            }

            return messagesModel;
        }

        [HttpGet]
        public List<MessageWithGuidModel> SearchByReceiver(Guid receiverId)
        {
            var messages = messageSearchingService.SearchByReceiver(receiverId);
            List<MessageWithGuidModel> messagesModel = new List<MessageWithGuidModel>();
            foreach (var msg in messages)
            {
                messagesModel.Add(new MessageWithGuidModel(msg));
            }

            return messagesModel;
        }

        [HttpGet]
        public List<MessageWithGuidModel> SearchByUser(Guid userId)
        {
            var messages = messageSearchingService.SearchByUser(userId);
            List<MessageWithGuidModel> messagesModel = new List<MessageWithGuidModel>();
            foreach (var msg in messages)
            {
                messagesModel.Add(new MessageWithGuidModel(msg));
            }

            return messagesModel;
        }

        [HttpGet]
        public IEnumerable<MessageWithGuidModel> SearchByText(string text)
        {
            var messages = messageSearchingService.SearchByText(text);
            List<MessageWithGuidModel> messagesModel = new List<MessageWithGuidModel>();
            foreach (var msg in messages)
            {
                messagesModel.Add(new MessageWithGuidModel(msg));
            }

            return messagesModel;
        }
    }
}