using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.DataTransferObjects
{
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