using System;
using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.PhotoService
{
    public class UsersPhotoService
    {
        private Random rand = new Random();

        public ProfilePhoto GetRandomPhoto()
        {
            int randNumber = rand.Next(0, 12);
            return new ProfilePhoto { Url = string.Format(@"..\Resources\images\ProfilePhotos\{0}.jpg", randNumber) };
        }

        public ProfilePhoto GetProfilePhoto(Guid id)
        {
            using (var unit = new UnitOfWork())
            {
                return unit.ProfilePhotos.GetAll().FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
