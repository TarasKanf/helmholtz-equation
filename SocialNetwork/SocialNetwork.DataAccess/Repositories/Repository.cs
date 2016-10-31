using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.DataAccess.Context;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private SocialNetworkContext db;

        public Repository(SocialNetworkContext context)
        {
            this.Db = context;
        }

        protected SocialNetworkContext Db
        {
            get
            {
                return db;
            }

            set
            {
                db = value;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().AsEnumerable();
        }

        public TEntity Get(Guid id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Create(TEntity item)
        {
            Db.Set<TEntity>().Add(item);
        }

        public void Update(TEntity item)
        {
            Db.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            TEntity item = Db.Set<TEntity>().Find(id);
            if (item != null)
            {
                Db.Set<TEntity>().Remove(item);
            }
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
