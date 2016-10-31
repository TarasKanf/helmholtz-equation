using System.Text;
using SocialNetwork.Bot.Bot2Helpers;

namespace SocialNetwork.Bot.Handlers
{
    internal class HelpHandler : IHandler
    {
        private readonly string[] availableCommands;

        public HelpHandler(string[] commands)
        {
            availableCommands = commands;
        }

        public string Usage { get; } = "help";

        public string GetAnswer(RequestInfo info)
        {
            var answer = new StringBuilder();
            foreach (string s in availableCommands)
            {
                answer.Append($"{s} \n");
            }

            return answer.ToString();
        }
    }
}