using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Entities
{
    public class Connection: BaseEntity
    {
        public Guid FirstUserId { get; set; }

        public Guid SecondUserId { get; set; }

        public Connection():
            base()
        {
        }
    }
}
