using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Context
{
    public class SocialNetworkContext : DbContext
    {        
        public SocialNetworkContext() :
            base("SocialNetworkDb")
        {
            Database.SetInitializer<SocialNetworkContext>(new SocialNetworkContextInitializer());
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<HandlerType> HandlerTypes { get; set; }

        public DbSet<HashAnswer> HashAnswerss { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
