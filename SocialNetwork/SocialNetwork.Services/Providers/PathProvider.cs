using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace SocialNetwork.Services.Providers
{
    public class PathProvider : IPathProvider
    {
        public string MapPath(string folder, string name)
        {
            string path = HostingEnvironment.MapPath("~/" + folder + "/" + name);

            return path;
        }
    }
}