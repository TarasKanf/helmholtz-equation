using System;
using System.Collections.Generic;

namespace SocialNetwork.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(Guid id);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(Guid id);
    }
}