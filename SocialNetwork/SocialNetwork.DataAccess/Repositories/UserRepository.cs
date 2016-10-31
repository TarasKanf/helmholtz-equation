using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.DataAccess.Context;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SocialNetworkContext context) :
            base(context)
        {
        }

        #region IUserStore
        public Task CreateAsync(User user)
        {
            Db.Users.Add(user);
            return Task.FromResult(1);
        }

        public Task DeleteAsync(User user)
        {
            Db.Set<User>().Remove(user);
            return Task.FromResult(1);
        }

        public Task<User> FindByIdAsync(Guid userId)
        {
            return Db.Set<User>().FindAsync(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Db.Users.FirstOrDefaultAsync(t => t.UserName == userName);
        }

        public Task UpdateAsync(User user)
        {
            Db.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
            return Task.FromResult(1);
        }

        public void Dispose()
        {
        }
        #endregion
        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.HashPassword);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.HashPassword));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.HashPassword = passwordHash;
            return Task.FromResult(1);
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(User user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(1);
        }
        #endregion

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult<bool>(true);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            return Task.FromResult(1);
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            user.Roles.Add(Db.Roles.FirstAsync(t => t.Name == roleName).Result);
            return Task.FromResult(1);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            user.Roles.Remove(Db.Roles.FirstAsync(t => t.Name == roleName).Result);
            return Task.FromResult(1);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            List<string> rolesNames = new List<string>();
            foreach (var role in user.Roles)
            {
                rolesNames.Add(role.Name);
            }

            return Task.FromResult<IList<string>>(rolesNames);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            List<string> userRoles = GetRolesAsync(user).Result.ToList();
            return Task.FromResult<bool>(userRoles.Contains(roleName));
        }
    }
}
