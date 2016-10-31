using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Entities
{
    public class Image : BaseEntity
    {
        public Image()
        {
        }

        [Required]
        public string Url { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
