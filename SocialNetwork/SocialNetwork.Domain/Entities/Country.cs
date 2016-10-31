using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Serializable]
    public class Country : BaseEntity
    {
        public Country()
        {
        }

        [Required]
        public string Name { get; set; }      
    }
}
