using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Contracts
{
    public interface IImageService
    {
        ProfilePhoto Create(Guid userId, Guid photoId);

        void CreatePhoto(Guid id, string url);

        FileStream GetImageById(Guid id);
    }
}
