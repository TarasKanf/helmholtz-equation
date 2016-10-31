using SocialNetwork.Bot.Bot2Helpers;

namespace SocialNetwork.Bot.Handlers
{
    internal interface IHandler
    {
        string Usage { get; }

        string GetAnswer(RequestInfo info);
    }
}