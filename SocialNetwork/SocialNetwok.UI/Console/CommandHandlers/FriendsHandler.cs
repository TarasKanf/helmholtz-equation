using System.Collections;
using System.Collections.Generic;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.Properties;
using SocialNetwork.Web.WebApiWrapper;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class FriendsHandler : CommandHandler
    {
        private readonly FriendsService friendsService;

        public FriendsHandler()
        {
            friendsService = NinjectKernel.Get<FriendsService>();
        }

        /// <summary>
        ///     Prints all mutual friends between logged in user and user specified by email,
        ///     if commandInfo containes option mutual = true and email is valid.
        ///     Elsewhere it prints all friends of current user
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            Response<List<UserModel>> response = null;

            bool isEmail = !string.IsNullOrEmpty(commandInfo.Email);
            if (commandInfo.Mutual && isEmail)
            {
                response = friendsService.GetMutualFriends(
                    Session.LoggedUser.Id,
                    commandInfo.Email);
                if (response.IsSuccessful)
                {
                    PrintFriends(response.ResultTask.Result);
                }

                return true;
            }

            if (!(commandInfo.Mutual ^ !isEmail))
            {
                IoService.Out.WriteLine(Resources.ErrorWrongParams);

                return false;
            }

            response = friendsService.GetFriends(Session.LoggedUser.Id);
            if (response.IsSuccessful)
            {
                PrintFriends(response.ResultTask.Result);
            }

            return true;
        }

        private void PrintFriends(IEnumerable friends)
        {
            foreach (var friend in friends)
            {
                IoService.Out.WriteLine(
                    friend.ToString());
            }
        }
    }
}