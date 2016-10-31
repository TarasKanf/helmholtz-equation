using SocialNetwork.UI.Console.CommandInfos;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal interface ICommandHandler
    {
        bool Execute(CommandInfo commandInfo);
    }
}
