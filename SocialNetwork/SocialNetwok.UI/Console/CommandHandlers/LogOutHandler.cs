using System;
using SocialNetwork.Common;
using SocialNetwork.Services;
using SocialNetwork.UI.Console.CommandInfos;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class LogOutHandler : CommandHandler
    {
        private static readonly Logger Logger = new Logger();
        private readonly AuthenticationService authenService;
        
        public LogOutHandler()
        {
            authenService = NinjectKernel.Get<AuthenticationService>();
        }

        public event EventHandler LoggedOut;

        /// <summary>
        ///     Logs out user and deletes user from currentSession
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            LogOut();
            LoggedOut?.Invoke(this, new EventArgs());

            return true;
        }

        private void LogOut()
        {
            try
            {
                authenService.LogOut(SessionKey);
            }
            catch (Exception ex)
            {
                IoService.Out.WriteLine(ex.Message);
                Logger.Error(ex.Message);
            }
        }
    }
}
