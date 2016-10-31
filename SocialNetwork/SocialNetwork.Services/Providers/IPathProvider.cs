using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Providers
{
    public interface IPathProvider
    {
        string MapPath(string folder, string name);
    }
}
