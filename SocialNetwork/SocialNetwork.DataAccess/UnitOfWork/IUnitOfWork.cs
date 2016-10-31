using System;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.UnitOfWork
{ 
    public interface IUnitOfWork : IDisposable
    {
        UserRepository Users { get; }

        IRepository<Message> Messages { get; }

        IRepository<Connection> Connections { get; }

        IRepository<City> Cities { get; }

        IRepository<Country> Countries { get; }

        IRepository<HandlerType> HandlerTypes { get; }

        IRepository<HashAnswer> HashAnswers { get; }

        IRepository<Image> Images { get; }

        RoleRepository Roles { get; }

        IRepository<Location> Locations { get; }

        IRepository<ProfilePhoto> ProfilePhotos { get; }

        void Save();
    }
}
