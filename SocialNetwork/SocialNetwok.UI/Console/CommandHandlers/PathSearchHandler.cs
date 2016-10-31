using System.Collections.Generic;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.Properties;
using SocialNetwork.Web.WebApiWrapper;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class PathSearchHandler : CommandHandler
    {
        private readonly ShortestUserPathService shortestUserPathService;

        public PathSearchHandler()
        {
            shortestUserPathService = NinjectKernel.Get<ShortestUserPathService>();
        }

        /// <summary>
        ///     Executes command 'path' appropriate to data stored in commandInfo
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            bool userExists = 
                commandInfo.Email != null;

            if (!userExists)
            {
                IoService.Out.WriteLine(Resources.ErrorUnknownUserEmail);

                return false;
            }

            Response<List<UserModel>> response = shortestUserPathService.GetShortestPath(Session.LoggedUser.Id, commandInfo.Email);
            if (response.IsSuccessful)
            {
                foreach (var user in response.ResultTask.Result)
                {
                    IoService.Out.WriteLine(user.ToString());
                }
            }
            else
            {
                IoService.Out.WriteLine(Resources.ErrorWhileDataTransfering);
            }

            return true;
        }
    }
}
