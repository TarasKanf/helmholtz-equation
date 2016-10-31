using System;
using SocialNetwork.Bot.Properties;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services;

namespace SocialNetwork.Bot
{
    internal class BotProfile : User
    {
        private const string BotStringForId = "ca761232ed4211cebacd00aa0057b223";

        public BotProfile()
        {
            Id = new Guid(BotStringForId);
            FirstName = Resources.BotFirstName;
            LastName = Resources.BotLastName;
            Email = Resources.BotEmail;
            HashPassword = string.Empty;
        }

        public static BotProfile GetBotProfileFromDb()
        {
            UserSearchingService userSearchingService = new UserSearchingService();
            var userBot = userSearchingService.SearchByEmail(Resources.BotEmail);

            return new BotProfile()
            {
                Id = userBot.Id,
                FirstName = userBot.FirstName,
                LastName = userBot.LastName,
                Email = userBot.Email,
                HashPassword = userBot.HashPassword
            };
        }
    }
}
