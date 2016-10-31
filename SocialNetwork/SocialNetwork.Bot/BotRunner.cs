using System.Threading;
using System.Threading.Tasks;
using SocialNetwork.Bot.Bots;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services;

namespace SocialNetwork.Bot
{
    public class BotRunner
    {
        private readonly AuthenticationService authentication;
        private readonly IBot bot;
        private SessionInfo sessionInfo;

        public BotRunner()
        {
            var botTemp = new Bot2();
            botTemp.BotProfile = BotProfile.GetBotProfileFromDb();
            bot = botTemp;

            authentication = new AuthenticationService();
        }

        private SessionInfo SessionInfo
        {
            set
            {
                sessionInfo = value;
                sessionInfo.NewMessageRecieved +=
                   (sender, args) => Task.Factory.StartNew(
                      () => OnNewMessageRecieved(args.Message),
                      CancellationToken.None,
                      TaskCreationOptions.None,
                      TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public void Run()
        {
            string sessionKey = AuthenticationService.SessionManager
                .CreateSession(bot.BotProfile);
            SessionInfo = authentication.GetSession(sessionKey);
        }

        private void OnNewMessageRecieved(Message message)
        {
            bot.HandleMessage(message);
        }
    }
}