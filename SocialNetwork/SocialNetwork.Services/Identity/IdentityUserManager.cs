using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Identity
{
    public class IdentityUserManager : UserManager<User, Guid>
    {
        public IdentityUserManager(UserRepository userRepository)
            : base(userRepository)
        {
            this.UserValidator = new UserValidator<User, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
        }

        public Task SendAsync()
        {
            return Task.FromResult(1);
        }
    }
}
