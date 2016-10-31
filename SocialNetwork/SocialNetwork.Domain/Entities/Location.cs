using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class Location : BaseEntity
    {
        public Location()
        {
        }

        [Required]
        public Guid CountryId { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }               
    }
}
