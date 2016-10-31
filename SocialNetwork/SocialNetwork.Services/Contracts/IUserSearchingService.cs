using System;
using System.Collections.Generic;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.Contracts
{
    public interface IUserSearchingService
    {
        User SearchById(Guid id);

        User SearchByEmail(string email);

        User SearchByLoginOrEmail(string mailOrLogin);

        IEnumerable<User> SearchByName(string firstName, string lastName);

        IEnumerable<User> SearchByFirstName(string firstName);

        IEnumerable<User> SearchByLastName(string lastName);
    }
}