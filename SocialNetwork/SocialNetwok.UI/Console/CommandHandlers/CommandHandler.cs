using System;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services;
using SocialNetwork.Services.Authentication;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.InputOutput;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal abstract class CommandHandler : ICommandHandler
    {       
        private static SessionInfo session;

        protected CommandHandler()
        {
            IoService = NinjectKernel.Get<IInputOutputSevice>();
            Session = new SessionInfo();
        }

        /// <summary>
        /// Containes current session logged in user
        /// </summary>
        public static SessionInfo Session
        {
            get
            {
                return session;
            }

            set
            {
                session = value;
                session.NewMessageRecieved += PrintNotifingAboutNewMessage;
            }
        }

        /// <summary>
        /// Key to get current SessionInfo if it wasn`t closed from server
        /// </summary>
        protected static string SessionKey { get; set; } = string.Empty;

        /// <summary>
        /// Prints and reads lines, passwords from UI
        /// </summary>
        protected static IInputOutputSevice IoService { get; set; }

        /// <summary>
        /// Executes command with specified name and command options stored in commandInfo 
        /// </summary>
        /// <param name="commandInfo"> Stores information about command and its options </param>
        /// <returns></returns>
        public abstract bool Execute(CommandInfo commandInfo);

        private static void PrintNotifingAboutNewMessage(object sender, SessionInfo.RecievedMessageEventArgs e)
        {
            IoService.Out.WriteLine("You have new message from {0}", e.Message.SenderId);
        }
    }
}