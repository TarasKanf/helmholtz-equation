using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Context;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleStore<Role, Guid>
    {
        public RoleRepository(SocialNetworkContext context) :
            base(context)
        {
        }

        public Task CreateAsync(Role role)
        {
            Db.Roles.Add(role);
            return Task.FromResult(1);
        }

        public Task DeleteAsync(Role role)
        {
            Db.Set<Role>().Remove(role);
            return Task.FromResult(1);
        }

        public void Dispose()
        {
        }

        public Task<Role> FindByIdAsync(Guid roleId)
        {
            return Db.Set<Role>().FindAsync(roleId);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Db.Roles.FirstOrDefaultAsync(t => t.Name == roleName);
        }

        public Task UpdateAsync(Role role)
        {
            Db.Entry<Role>(role).State = System.Data.Entity.EntityState.Modified;
            return Task.FromResult(1);
        }
    }
}
