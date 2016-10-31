using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Domain.DataTransferObjects
{
    public class FriendsModel
    {
        [Required]
        public Guid UserId1 { get; set; }

        [Required]
        public Guid UserId2 { get; set; }
    }
}