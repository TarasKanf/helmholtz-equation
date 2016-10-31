using System.Collections;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.Properties;
using SocialNetwork.Web.WebApiWrapper;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class FindHandler : CommandHandler
    {
        private readonly UserSearcher userSearchService;

        public FindHandler()
        {
            userSearchService = NinjectKernel.Get<UserSearcher>();
        }

        /// <summary>
        ///     Executes 'find' command appropriate to data stored in commandInfo
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            if (commandInfo.User && commandInfo.Email != null)
            {
                Response<UserModel> response = userSearchService.GetByEmail(commandInfo.Email);

                UserModel user = null;
                if (response.IsSuccessful)
                {
                    user = response.ResultTask.Result;
                }

                IoService.Out.WriteLine(
                    user?.ToString() 
                    ?? Resources.ErrorUnknownUserEmail);
            }

            if (!commandInfo.Message)
            {
                return true;
            }

            // TODO Searching of messages
            return true;
        }
    }
}
