using System.Net;

namespace SocialNetwork.Services.Contracts
{
    public interface IEndPointSetter
    {
        void SetEndPoint(string sessionKey, IPEndPoint endPoint);
    }
}