using System;
using SocialNetwork.Common;
using SocialNetwork.Services;
using SocialNetwork.UI.Console.CommandInfos;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class ExitHandler : CommandHandler
    {
        private static readonly Logger Logger = new Logger();

        /// <summary>
        ///     Invokes when exit command was done.
        /// </summary>
        public event EventHandler Exited;

        /// <summary>
        ///     Executes 'exit' command
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            Logger.Info($"User {Session.LoggedUser.Email} exited");

            var authenService = NinjectKernel.Get<AuthenticationService>();
            authenService.LogOut(SessionKey);

            Exited?.Invoke(this, new EventArgs());

            return true;
        }
    }
}