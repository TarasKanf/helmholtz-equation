using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class HandlerType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
