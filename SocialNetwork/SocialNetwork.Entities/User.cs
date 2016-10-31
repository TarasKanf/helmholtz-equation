using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Entities
{
    public class User: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Photo { get; set; }

        public User()
        {
        }

        public User(string fname, string lname, string email, string pass, string photo)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            Password = pass;
            Photo = photo;
        }
    }
}
