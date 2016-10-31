using System.Collections.Generic;
using System.Net.Sockets;

namespace SocialNetwork.Web.WebApiWrapper.Helpers
{
    internal class StateObject
    {
        // Size of receive buffer.
        public const int BufferSize = 8192;
        // Receive buffer.
        public byte[] Buffer = new byte[BufferSize];

        public List<byte> Bytes = new List<byte>();
        // Client  socket.
        public Socket WorkSocket;
    }
}