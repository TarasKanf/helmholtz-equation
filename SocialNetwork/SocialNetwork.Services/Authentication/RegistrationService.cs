using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SocialNetwork.Common;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Identity;
using SocialNetwork.Services.PhotoService;
using SocialNetwork.Services.Properties;

namespace SocialNetwork.Services
{
    public class RegistrationService
    {
        private static readonly Logger Logger = new Logger(typeof(RegistrationService).FullName);

        public static async Task<IdentityResult> Registration(SocialNetwork.Domain.DataTransferObjects.RegistrationDTO model)
        {
            using (var work = new UnitOfWork())
            {
                List<Role> roles = new List<Role>();
                roles.Add(work.Roles.FindByNameAsync("User").Result);

                ProfilePhoto photo = (new UsersPhotoService()).GetRandomPhoto();
                var user = new User(model.FirstName, model.LastName, model.Email, photo)
                {
                    LocationId = work.Locations.GetAll().First().Id,
                    UserName = model.UserName,
                    Birthdate = DateTime.Now,
                    Roles = roles
                };

                try
                {
                    IdentityUserManager userManager = new IdentityUserManager(work.Users);
                    var res = await userManager.CreateAsync(user, model.Password);
                    work.Save();
                    return res;
                }
                catch (Exception)
                {                    
                    return null;
                }
            }
        }

        public bool Register(string fname, string lname, string email, string password)
        {
            using (var work = new UnitOfWork())
            {
                if (work.Users.GetAll().FirstOrDefault(x => x.Email == email) != null)
                {
                    Logger.Error(string.Format(Resources.LogRegisterExistingEmail, email));
                    return false;
                }

                var hash = new Md5CryptoService();

                ProfilePhoto photo = (new UsersPhotoService()).GetRandomPhoto();
                var user = new User(fname, lname, email, password, photo)
                {
                    HashPassword = hash.CalculateMd5Hash(password),
                    LocationId = work.Locations.GetAll().First().Id,
                    UserName = email,
                    Birthdate = DateTime.Now
                };

                work.Users.Create(user);
                work.Save();
                List<User> users = work.Users.GetAll().ToList();
                Logger.Info(string.Format(Resources.RegistrationFinished, email));

                return true;
            }
        }

        public bool DeleteProfile(Guid id)
        {
            using (var work = new UnitOfWork())
            {
                work.Users.Delete(id);
                work.Save();

                Logger.Info(string.Format(Resources.ProfileDeleted, id));

                return true;
            }
        }
    }
}