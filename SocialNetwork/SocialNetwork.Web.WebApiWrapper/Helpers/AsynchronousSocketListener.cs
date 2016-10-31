using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocialNetwork.Web.WebApiWrapper.Helpers
{
    internal class AsynchronousSocketListener
    {
        // Thread signal.
        public static ManualResetEvent AllDone = new ManualResetEvent(false);

        public IPEndPoint LocalEndPoint { get; set; }

        public event EventHandler<byte[]> AllBytesRecieved;

        public void StartListening()
        {
            // Create a TCP/IP socket.
            var listener = new Socket(LocalEndPoint.Address.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(LocalEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    AllDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    listener.BeginAccept(
                        AcceptCallback,
                        listener);

                    // Wait until a connection is made before continuing.
                    AllDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            AllDone.Set();

            // Get the socket that handles the client request.
            var listener = (Socket) ar.AsyncState;
            var handler = listener.EndAccept(ar);

            // Create the state object.
            var state = new StateObject();
            state.WorkSocket = handler;
            handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                ReadCallback, state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            var state = (StateObject) ar.AsyncState;
            var handler = state.WorkSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead <= state.Buffer.Length)
            {
                // There  might be more data, so store the data received so far.
                for (var i = 0; i < bytesRead; i++)
                {
                    state.Bytes.Add(state.Buffer[i]);
                }

                // Echo the data back to the client.
                //Send(handler, "Message was recieved");
                OnAllBytesRecieved(state.Bytes.ToArray());
            }
            else
            {
                // There might be more data, so store the data received so far.
                // Get the rest of the data.
                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    ReadCallback, state);
            }
        }

        private void Send(Socket handler, string data)
        {
            // Convert the string data to byte data using ASCII encoding.
            var byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                SendCallback, handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                var handler = (Socket) ar.AsyncState;

                // Complete sending the data to the remote device.
                handler.EndSend(ar);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected void OnAllBytesRecieved(byte[] e)
        {
            AllBytesRecieved?.Invoke(this, e);
        }
    }
}