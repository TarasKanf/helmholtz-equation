using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace SocialNetwork.Web.WebApi.Controllers.WebApi
{
    public class ImageController : ApiController
    {
        private IImageService imageService;
        
        public ImageController(IImageService service)
        {
            imageService = service;
        }

        [HttpPost]
        public async Task<Guid> Create()
        {
            Guid? Id = null;
            if (Request.Content.IsMimeMultipartContent())
            {               
                MultipartMemoryStreamProvider provider = await Request.Content
                    .ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider());
                foreach (HttpContent content in provider.Contents)
                {
                    Stream stream = content.ReadAsStreamAsync().Result;
                    Image image = Image.FromStream(stream);
                    String filePath = HostingEnvironment.MapPath("~/Content/Images");
                    Id = Guid.NewGuid();
                    string imageName = Id.ToString() + ".jpg";
                    String fullPath = Path.Combine(filePath, imageName);
                    image.Save(fullPath);

                    imageService.CreatePhoto(Id.Value, fullPath);

                    //only one!!!
                    break;
                }
            }

            return Id.Value;
        }

        public HttpResponseMessage Get(Guid id)
        {
            var file = imageService.GetImageById(id);

            HttpResponseMessage response = null;

            if (file != null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(file);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return response;
        }

    }
}
