using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Web.WebApiWrapper.Helpers;
using SocialNetwork.Web.WebApiWrapper.Properties;

namespace SocialNetwork.Web.WebApiWrapper.Services
{
    public class ServerListener : BaseService
    {
        private readonly AsynchronousSocketListener listener = new AsynchronousSocketListener();
        private IPEndPoint localEndPoint;

        public SessionInfo Session { get; set; }

        private object locker = new object();

        public async Task<Response> Connect(string sessionKey)
        {
            if (sessionKey == null)
            {
                return new Response();
            }
            var ipHostInfo = Dns.GetHostEntry("localhost");
            var ipAddress = ipHostInfo.AddressList[0];
            var endPoint = new IPEndPoint(ipAddress, 11000);
            localEndPoint = endPoint;

            var clientEndPoint = new ClientEndPoint
            {
                SessionKey = sessionKey,
                EndPoint = endPoint
            };

            return await PutDataToRoute(Host + Resources.RouteSetEndPoint, clientEndPoint);
        }

        public void StartListening()
        {
            listener.LocalEndPoint = localEndPoint;
            listener.AllBytesRecieved +=
                (sender, bytes) => Task.Run(() => RecieveMessage(bytes));

            listener.StartListening();
        }

        private void RecieveMessage(byte[] bytes)
        {
            try
            {
                Message messageRecieved;
                lock (locker)
                {
                    Message message;
                    var formatter = new BinaryFormatter();
                    using (var ms = new MemoryStream(bytes))
                    {
                        ms.Position = 0;
                        message = (Message) formatter.Deserialize(ms);
                    }

                    messageRecieved = message;
                    Session?.RecieveNewMessage(messageRecieved);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}