using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Services.Providers;

namespace SocialNetwork.Services.PhotoService
{
    public class ImageService : IImageService
    {
        //// private readonly IPathProvider pathProvider;

        //// public ImageService(IPathProvider pathProvider)
        //// {
        ////    this.pathProvider = pathProvider;
        //// }

        public ProfilePhoto Create(Guid photoId, Guid userId)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                ProfilePhoto image = work.ProfilePhotos.Get(photoId);
                var user = work.Users.Get(userId);
                if (user.ProfilePhotoId != null && work.ProfilePhotos.Get(user.ProfilePhotoId) != null)
                {
                    DeletePhoto(user.ProfilePhotoId);
                }

                user.ProfilePhotoId = image.Id;
                work.Save();

                return image;
            }
        }

        public void CreatePhoto(Guid id, string url)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                work.ProfilePhotos.Create(new Domain.Entities.ProfilePhoto
                {
                    Id = id,
                    Url = url
                });

                work.Save();
            }
        }

        public void DeletePhoto(Guid id)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                var entity = work.ProfilePhotos.Get(id);
                
                if (entity != null)
                {
                    if (File.Exists(entity.Url))
                    {
                        File.Delete(entity.Url);
                    }

                    work.ProfilePhotos.Delete(entity.Id);
                }
            }
        }

        public FileStream GetImageById(Guid id)
        {
            using (var uow = new UnitOfWork())
            {
                var image = uow.ProfilePhotos.Get(id);
                if (image != null)
                {
                    return new FileStream(image.Url, FileMode.Open, FileAccess.Read);
                }

                return null;
            }
        }
    }
}
