using System;
using System.Data.Entity;
using System.Linq;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Context
{
    internal class SocialNetworkContextInitializer : DropCreateDatabaseIfModelChanges<SocialNetworkContext>
    {
        protected override void Seed(SocialNetworkContext db)
        {
            base.Seed(db);
            GenerateLocations(db);
            GenerateBot(db);
            InitializeRoles(db);
        }

        private void GenerateBot(SocialNetworkContext db)
        {
            Location l = db.Locations.FirstOrDefaultAsync().Result;

            ProfilePhoto botPhoto = new ProfilePhoto
            {
                Url = @"..\Resources\Images\ProfilePhotos\bot.png"
            };

            db.ProfilePhotos.Add(botPhoto);

            User bot =
                new User
                {
                    Email = "bot@gmail.com",
                    FirstName = "Botty",
                    LastName = "Bot",
                    UserName = "botty",
                    HashPassword = "------",
                    Birthdate = DateTime.Now,
                    ProfilePhotoId = botPhoto.Id,
                    LocationId = l.Id
                };

            db.Users.Add(bot);

            db.SaveChanges();
        }

        private void GenerateLocations(SocialNetworkContext db)
        {
            City lviv = new City { Name = "Lviv" };
            City kyiv = new City { Name = "Kyiv" };
            City ternopil = new City { Name = "Ternopil" };
            Country ukraine = new Country { Name = "Ukraine" };
            db.Locations.Add(new Location { City = lviv, Country = ukraine });
            db.Locations.Add(new Location { City = kyiv, Country = ukraine });
            db.Locations.Add(new Location { City = ternopil, Country = ukraine });
            db.SaveChanges();
        }

        private void InitializeRoles(SocialNetworkContext db)
        {
            db.Roles.Add(new Role { Name = "Admin" });
            db.Roles.Add(new Role { Name = "User" });

            db.SaveChanges();
        }

        /*private void InitializeJokes(SocialNetworkContext db)
        {
            db.HashAnswerss.Add(new HashAnswer("It's hard to explain puns to kleptomaniacs because they always take things literally."));
        }*/
    }
}