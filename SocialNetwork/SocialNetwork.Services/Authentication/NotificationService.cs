using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SocialNetwork.Common;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Authentication
{
    internal class NotificationService
    {
        private static readonly Logger Logger =
            new Logger(typeof(NotificationService).FullName);

        private object locker = new object();

        private IPEndPoint endPoint;

        private Message message;

        public NotificationService()
        {
            lock (locker)
            {
                NotificationMethod += (sender, args) => Task.Run(
                     () => SendMessageToEndPoint(args.Message, args.EndPoint));
            }
        }

        public EventHandler<SessionInfo.RecievedMessageEventArgs> NotificationMethod { get; set; }
            
        private void SendMessageToEndPoint(Message message, IPEndPoint endPoint)
        {
            if ((endPoint == null) || (message == null))
            {
                return;
            }

            this.message = message;
            this.endPoint = endPoint;
            SendMessage();
        }

        private void SendMessage()
        {
            // Data buffer for incoming data.
            var bytes = new byte[8192];

            // Connect to a remote device.
            try
            {
                var disconnected = false;
                while (!disconnected)
                {
                    disconnected = true;

                    // Create a TCP/IP  socket.
                    var sender = new Socket(
                                                endPoint.Address.AddressFamily,
                                                         SocketType.Stream, 
                                                          ProtocolType.Tcp);

                    // Connect the socket to the remote endpoint. Catch any errors.
                    try
                    {
                        sender.Connect(endPoint);

                        var msg = MessageToByteArray(message);

                        //// Send the data through the socket.
                        int bytesSent = sender.Send(msg);

                        //// Receive the response from the remote device.
                        ////int bytesRec = sender.Receive(bytes);
                        //// TODO send again if message wasnt send properly

                        //// Release the socket.
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }
                    catch (ArgumentNullException ane)
                    {
                        Logger.Error($"ArgumentNullException : {ane}");
                    }
                    catch (SocketException se)
                    {
                        Logger.Error($"SocketException : {se}");
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Unexpected exception : {e}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private byte[] MessageToByteArray(Message obj)
        {
            if (obj == null)
            {
                return null;
            }

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}