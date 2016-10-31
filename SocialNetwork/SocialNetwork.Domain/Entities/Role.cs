using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.Domain.Entities
{
    public class Role : BaseEntity, IRole<Guid>
    {
        [Required]
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}