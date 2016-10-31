using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.DataTransferObjects
{
    public class SessionInfo
    {
        public SessionInfo()
        {
            IsLogged = false;
        }

        public SessionInfo(UserModel user, DateTime time)
        {
            LoggedUser = user;
            LogInTime = time;
            IsLogged = false;
        }

        public event EventHandler<RecievedMessageEventArgs> NewMessageRecieved;

        public bool IsLogged { get; set; }

        [Required]
        public string SessionKey { get; set; }

        // TODO add logic of using sockets to be notified

        /// <summary>
        ///     IPEndPoint that client listens to in order to be notified about new messages
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; set; }

        public UserModel LoggedUser { get; set; }

        public DateTime LogInTime { get; set; }

        internal void RecieveNewMessage(Message message)
        {
            NewMessageRecieved?.Invoke(
                this,
                new RecievedMessageEventArgs(message, RemoteEndPoint));
        }

        public class RecievedMessageEventArgs : EventArgs
        {
            public RecievedMessageEventArgs(Message message, IPEndPoint remoteEndPoint)
            {
                Message = message;
                EndPoint = remoteEndPoint;
            }

            public Message Message { get; }

            public IPEndPoint EndPoint { get; }
        }
    }
}