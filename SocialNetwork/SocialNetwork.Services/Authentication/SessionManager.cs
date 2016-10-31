using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Authentication
{
    public class SessionManager
    {
        private readonly Dictionary<string, SessionInfo> sessions;

        public SessionManager()
        {
            sessions = new Dictionary<string, SessionInfo>();
        }

        public SessionInfo GetSessionByEmail(string email)
        {
            return sessions.Values.FirstOrDefault(x => x.LoggedUser.Email == email);
        }

        public SessionInfo GetSession(string key)
        {
            SessionInfo session = new SessionInfo();
            try
            {
                session = sessions[key];
            }
            catch (Exception)
            {
                return session;
            }

            return session;
        }

        internal string CreateSession(User user)
        {
            var creatingTime = DateTime.Now;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(creatingTime);
            stringBuilder.Append(user.Email);
            stringBuilder.Append(user.HashPassword);

            string input = stringBuilder.ToString();
            string key = new Md5CryptoService().CalculateMd5Hash(input);

            var session = new SessionInfo(new UserModel(user), creatingTime) { IsLogged = true };
            session.SessionKey = key;
            sessions.Add(key, session);

            return key;
        }

        internal void DeleteSession(string sessionKey)
        {
           sessions.Remove(sessionKey);
        }

        internal void NotifySessionAboutMessage(Message message)
        {
            var sessionsToBeNotified = from s in sessions.Values where s.LoggedUser.Id == message.ReceiverId select s;
            foreach (var s in sessionsToBeNotified)
            {
                s?.RecieveNewMessage(message);
            }
        }
    }
}