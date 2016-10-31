using System;
using Microsoft.AspNet.Identity;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Repositories
{
    public interface IUserRepository : IUserStore<User, Guid>, IUserPasswordStore<User, Guid>,
            IUserSecurityStampStore<User, Guid>, IUserRoleStore<User, Guid>, IRepository<User>
    {
    }
}
