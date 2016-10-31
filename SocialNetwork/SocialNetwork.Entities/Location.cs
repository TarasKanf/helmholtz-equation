using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Entities
{
    public class Location: BaseEntity
    {
        [Required]
        public Guid CountryId { get; set; }

        public Guid CityId { get; set;}

        public Location():
            base()
        {

        }
    }
}
