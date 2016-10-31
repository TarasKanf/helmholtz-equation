using System;
using SocialNetwork.DataAccess.Context;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.UnitOfWork
{
    /// <summary>
    ///     Contains all messages, users, connections;
    /// </summary> 
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialNetworkContext context;
        private IRepository<Connection> connectionRepository;
        private IRepository<Message> messageRepository;
        private UserRepository userRepository;
        private IRepository<City> cityRepository;
        private IRepository<Country> countryRepository;
        private IRepository<HandlerType> handlerTypeRepository;
        private IRepository<HashAnswer> hashAnswerRepository;
        private IRepository<Image> imageRepository;
        private IRepository<Location> locationRepository;
        private IRepository<ProfilePhoto> profilePhotoRepository;
        private RoleRepository roleRepository;
        private bool disposed = false;

        public UnitOfWork()
        {
            context = new SocialNetworkContext();
        }

        public UnitOfWork(SocialNetworkContext xmlContext)
        {
            context = xmlContext;
        }

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }

                return userRepository;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (messageRepository == null)
                {
                    messageRepository = new Repository<Message>(context);
                }

                return messageRepository;
            }
        }

        public IRepository<Connection> Connections
        {
            get
            {
                if (connectionRepository == null)
                {
                    connectionRepository = new Repository<Connection>(context);
                }

                return connectionRepository;
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                if (cityRepository == null)
                {
                    cityRepository = new Repository<City>(context);
                }

                return cityRepository;
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                if (countryRepository == null)
                {
                    countryRepository = new Repository<Country>(context);
                }

                return countryRepository;
            }
        }

        public IRepository<HandlerType> HandlerTypes
        {
            get
            {
                if (handlerTypeRepository == null)
                {
                    handlerTypeRepository = new Repository<HandlerType>(context);
                }

                return handlerTypeRepository;
            }
        }

        public IRepository<HashAnswer> HashAnswers
        {
            get
            {
                if (hashAnswerRepository == null)
                {
                    hashAnswerRepository = new Repository<HashAnswer>(context);
                }

                return hashAnswerRepository;
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                if (imageRepository == null)
                {
                    imageRepository = new Repository<Image>(context);
                }

                return imageRepository;
            }
        }

        public RoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(context);
                }

                return roleRepository;
            }
        }

        public IRepository<Location> Locations
        {
            get
            {
                if (locationRepository == null)
                {
                    locationRepository = new Repository<Location>(context);
                }

                return locationRepository;
            }
        }

       public IRepository<ProfilePhoto> ProfilePhotos
        {
            get
            {
                if (profilePhotoRepository == null)
                {
                    profilePhotoRepository = new Repository<ProfilePhoto>(context);
                }
                    
                return profilePhotoRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}