using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User(string fname, string lname, string email, string pass)
        {
            Id = Guid.NewGuid();
            FirstName = fname;
            LastName = lname;
            Email = email;
            Password = pass; // unsafe way to save password. Use System.Security.Cryptography to save passwords
        }
    }
}
