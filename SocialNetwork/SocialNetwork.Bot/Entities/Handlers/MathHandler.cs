using NCalc;
using SocialNetwork.Bot.Bot2Helpers;

namespace SocialNetwork.Bot.Handlers
{
    internal class MathHandler : IHandler
    {
        public string Usage { get; } = "<math expression>";

        public string GetAnswer(RequestInfo info)
        {
            var expression = new Expression(info.Data);
            string result = expression.Evaluate().ToString();
            return result;
        }
    }
}