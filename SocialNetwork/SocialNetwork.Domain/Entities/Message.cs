using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Domain.Entities
{
    [Serializable]
    public class Message : BaseEntity
    {
        public Message()
        {
        }

        public Message(Guid sender, Guid reciever, string data)
        {
            SenderId = sender;
            ReceiverId = reciever;
            Data = data;
            Date = DateTime.Now;
        }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public Guid ReceiverId { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }

        public override string ToString()
        {
            return string.Format(Data);
        }
    }
}