using System;
using System.Collections.Generic;
using System.Web.Http;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Web.WebApi.Models;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class MessageHistoryController : ApiController
    {
        private IMessageHistoryService messageHistoryService;

        public MessageHistoryController(IMessageHistoryService messageHistoryService)
        {
            this.messageHistoryService = messageHistoryService;
        }

        [HttpGet]
        public List<MessageWithGuidModel> GetAllMessageHistory(Guid userId, Guid userFriendId)
        {
            var messages = messageHistoryService.GetAllMessageHistory(userId, userFriendId);
            List<MessageWithGuidModel> messagesModel = new List<MessageWithGuidModel>();
            foreach (var msg in messages)
            {
                messagesModel.Add(new MessageWithGuidModel(msg));
            }

            return messagesModel;
        }

    }
}