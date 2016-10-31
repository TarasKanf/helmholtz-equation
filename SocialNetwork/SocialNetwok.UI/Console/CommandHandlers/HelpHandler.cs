using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.Properties;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class HelpHandler : CommandHandler
    {
        /// <summary>
        ///     Printes all commands that are available for user
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            if (Session.IsLogged)
            {
                PrintLoggedInCommands();
            }
            else
            {
                PrintNotLoggedCommands();
            }

            return true;
        }

        private void PrintLoggedInCommands()
        {
            IoService.Out.WriteLine(Resources.UsageFind);
            IoService.Out.WriteLine(Resources.UsageSend);
            IoService.Out.WriteLine(Resources.UsageExport);
            IoService.Out.WriteLine(Resources.UsageFriends);
            IoService.Out.WriteLine(Resources.UsagePath);
            IoService.Out.WriteLine(Resources.UsageHelp);
            IoService.Out.WriteLine(Resources.UsageLog);
            IoService.Out.WriteLine(Resources.UsageExit);
        }

        private void PrintNotLoggedCommands()
        {
            IoService.Out.WriteLine(Resources.UsageLog);
            IoService.Out.WriteLine(Resources.UsageSign);
            IoService.Out.WriteLine(Resources.UsageHelp);
        }
    }
}
