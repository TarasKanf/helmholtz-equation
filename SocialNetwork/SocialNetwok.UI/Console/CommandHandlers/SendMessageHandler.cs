using System;
using System.Text;
using SocialNetwork.Common;
using SocialNetwork.Services;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.Properties;
using SocialNetwork.Web.WebApiWrapper;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class SendMessageHandler : CommandHandler
    {
        private static readonly Logger Logger = new Logger();
        private readonly Messanger messanger = NinjectKernel.Get<Messanger>();

        /// <summary>
        ///     Executes command 'send' appropriately to data stored in commandInfo
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            // TODO validation: Does user with this email exists?
            SendMessage(commandInfo.Email);

            return true;
        }

        private void SendMessage(string recieverEmail)
        {
            IoService.Out.WriteLine(Resources.InfoMessageEntering + " // ");
            var stringBuilder = ReadText();

            try
            {
                Response response = messanger.SendAsync(
                      Session.LoggedUser.Email,
                      recieverEmail,
                      stringBuilder.ToString()).Result;
                if (response.IsSuccessful)
                {
                    IoService.Out.WriteLine(Resources.InfoMessageSuccessfullySend);
                }
                else
                {
                    IoService.Out.WriteLine(Resources.InfoMessageWasntSend);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                IoService.Out.WriteLine(ex.Message);
            }
        }

        private StringBuilder ReadText()
        {
            var messageEnded = false;
            var stringBuilder = new StringBuilder();

            while (!messageEnded)
            {
                string text = IoService.In.ReadLine();
                stringBuilder.Append(text);

                // recognize the end of message
                if (text?.Length >= 2 
                    && text[text.Length - 1] == '/' 
                    && text[text.Length - 2] == '/')
                {
                    messageEnded = true;
                    stringBuilder.Remove(stringBuilder.Length - 2, 2);
                }
            }

            return stringBuilder;
        }
    }
}