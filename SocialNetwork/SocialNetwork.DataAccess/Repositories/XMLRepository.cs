using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.DataAccess.Context;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Repositories
{
    /// <summary>
    ///     Make operations with data (Create, Delete, Get, GetAll, Update)
    /// </summary>
    /// <typeparam name="TEntity">BaseEntity: User, Connection, Message</typeparam>
    public class XmlRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private IContext xmlContext;

        public XmlRepository(IContext c)
        {
            XmlContext = c;
        }

        protected IContext XmlContext
        {
            get
            {
                return xmlContext;
            }

            set
            {
                xmlContext = value;
            }
        }

        public void Create(TEntity item)
        {
            var entities = XmlContext.Read<TEntity>();
            entities.Add(item);
            XmlContext.Write(entities);
        }

        public void Delete(Guid id)
        {
            var entities = XmlContext.Read<TEntity>();
            var entityToRemove = entities.FirstOrDefault(c => c.Id == id);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                XmlContext.Write(entities);
            }
        }

        public TEntity Get(Guid id)
        {
            var entities = XmlContext.Read<TEntity>();
            return entities.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return XmlContext.Read<TEntity>();
        }

        public void Update(TEntity item)
        {
            // XmlContext.Write(item);
        }
    }
}