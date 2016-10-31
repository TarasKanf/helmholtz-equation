using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(Guid id, string fname, string lname, string email, string pass)
        {
            Id = id;
            FirstName = fname;
            LastName = lname;
            Email = email;
            Password = pass;
        }
    }
}
