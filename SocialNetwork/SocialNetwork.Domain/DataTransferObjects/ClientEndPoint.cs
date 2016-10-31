using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SocialNetwork.Domain.DataTransferObjects
{
    public class ClientEndPoint
    {
        [Required]
        public string SessionKey { get; set; }

        [Required]
        public IPEndPoint EndPoint { get; set; }
    }
}