using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Bot
{
    internal interface IBot
    {
        BotProfile BotProfile { get; }

        void HandleMessage(Message message);
    }
}