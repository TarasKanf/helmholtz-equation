using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using SocialNetwork.Common;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services.Authentication;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Services.Properties;

namespace SocialNetwork.Services
{
    public class AuthenticationService : IEndPointSetter
    {
        internal static readonly SessionManager SessionManager =
            new SessionManager();

        private static readonly Logger Logger =
            new Logger(typeof(AuthenticationService).FullName);

        public void SetEndPoint(string sessionKey, IPEndPoint endPoint)
        {
            var session = SessionManager.GetSession(sessionKey);
            session.RemoteEndPoint = endPoint;
            session.NewMessageRecieved += new NotificationService().NotificationMethod;
        }

        /// <summary>
        ///     Log user out
        /// </summary>
        public void LogOut(string sessionKey)
        {
            var session = SessionManager.GetSession(sessionKey);
            Logger.Info(string.Format(
                Resources.UserLoggedOut,
                session.LoggedUser.Id));
            session.IsLogged = false;
            SessionManager.DeleteSession(sessionKey);
        }

        /// <summary>
        ///     Add user to current session if he is regisrated.
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="password"></param>
        /// <returns> Returns session key </returns>
        public string LogIn(string eMail, string password)
        {
            string sessionKey = null;
            using (var work = new UnitOfWork())
            {              
                string hashedPassword = password;

                var existingSession = SessionManager.GetSessionByEmail(eMail);
                if (existingSession != null)
                {
                    return existingSession.SessionKey;
                }

                var user = work.Users.GetAll().FirstOrDefault(
                    u => u.Email == eMail && u.HashPassword == hashedPassword);

                if (user != null)
                {
                    sessionKey = SessionManager.CreateSession(user);
                }

                return sessionKey;
            }
        }

        /// <summary>
        ///     Gets current session if user is logged in
        /// </summary>
        /// <param name="sessionKey"> Special session key to identify session</param>
        /// <returns></returns>
        public SessionInfo GetSession(string sessionKey)
        {
             return SessionManager.GetSession(sessionKey);
        }
    }
}