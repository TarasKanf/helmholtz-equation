using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.WebApi.Models
{
    public class FriendsModel
    {
        [Required]
        public Guid userId1;

        [Required]
        public Guid userId2;
    }
}