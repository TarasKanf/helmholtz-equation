using SocialNetwork.Web.WebApiWrapper.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class ImageUrlService : BaseService
    {
        public string GetImageUrl(Guid imageId)
        {
            return string.Format(Host + Resources.RouteGetImage, imageId);

        }
    }
}
