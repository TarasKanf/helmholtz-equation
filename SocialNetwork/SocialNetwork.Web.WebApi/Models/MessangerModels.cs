using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Web.WebApi.Models
{
    public class MessageWithGuidModel
    {
        public MessageWithGuidModel()
        {
        }

        public MessageWithGuidModel(Message message)
        {
            Data = message.Data;
            ReceiverId = message.ReceiverId;
            SenderId = message.SenderId;
        }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public Guid ReceiverId { get; set; }

        [Required]
        public string Data { get; set; }
    }

    public class MessageWithEmailModel
    {
        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public string ReceiverEmail { get; set; }

        [Required]
        public string Data { get; set; }
    }
}