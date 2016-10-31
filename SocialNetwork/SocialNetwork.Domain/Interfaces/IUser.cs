using System;

namespace SocialNetwork.Domain.Entities
{
    public interface IUser
    { 
        Guid Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        ProfilePhoto ProfilePhoto { get; set; }
    }
}