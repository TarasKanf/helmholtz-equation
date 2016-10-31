using System;
using SocialNetwork.Bot;
using SocialNetwork.Common;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.UI.Console.CommandHandlers;
using SocialNetwork.UI.Console.InputOutput;
using SocialNetwork.UI.Console.Properties;

namespace SocialNetwork.UI.Console
{
    internal static class Program
    {
        private static readonly Logger Logger = new Logger();
        private static EventHandler exitMethod;

        private static void Main()
        {
            using (var work = new UnitOfWork())
            {
                var res = work.Cities;
            }

            var iOService = NinjectKernel.Get<IInputOutputSevice>();

            // Run Bot1
            var botRunner = new BotRunner();
            botRunner.Run();

            iOService.Out.WriteLine(Resources.InfoGreeting);
            Logger.Info(Resources.InfoGreeting);

            Command authenHandlers = GetAuthenticationCommand();
            Command loggedInHandlers = GetLoggedInCommand();

            var exitCommand = false;
            exitMethod = (sender, e) => { exitCommand = true; };
            while (!exitCommand)
            {
                while (!CommandHandler.Session.IsLogged)
                {
                    iOService.Out.WriteLine(Resources.InfoFirsUsage);
                    iOService.Out.Write(Resources.InfoNewCommandLine);

                    authenHandlers.Execute(iOService.In.ReadLine());

                    iOService.Out.WriteLine();
                }

                iOService.Out.Write(Resources.InfoNewCommandLine);
                loggedInHandlers.Execute(iOService.In.ReadLine());

                iOService.Out.WriteLine();
            }
        }

        private static Command GetAuthenticationCommand()
        {
            var container = new Command();

            container.Handlers.Add(Resources.CommandLogIn, new LogInHandler());
            container.Handlers.Add(Resources.CommandSignUp, new SignUpHandler());
            container.Handlers.Add(Resources.CommandHelp, new HelpHandler());

            return container;
        }

        private static Command GetLoggedInCommand()
        {
            var container = new Command();

            container.Handlers.Add(Resources.CommandLogOut, new LogOutHandler());
            container.Handlers.Add(Resources.CommandHelp, new HelpHandler());
            container.Handlers.Add(Resources.CommandFind, new FindHandler());
            container.Handlers.Add(Resources.CommandSend, new SendMessageHandler());
            container.Handlers.Add(Resources.CommandExport, new ExportToCvsHandler());
            container.Handlers.Add(Resources.CommandFriends, new FriendsHandler());
            container.Handlers.Add(Resources.CommandPath, new PathSearchHandler());
            var exitHandler = new ExitHandler();
            exitHandler.Exited += exitMethod;
            container.Handlers.Add(Resources.CommandExit, exitHandler);

            return container;
        }
    }
}