using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Serializable]
    public class City : BaseEntity
    {
        public City()
        {
        }

        [Required]
        public string Name { get; set; }
    }
}
