using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.DataTransferObjects
{
    public class UserModel : IUser
    {
        public UserModel()
        {
        }

        public UserModel(User user)
        {
            if (user != null)
            {
                FirstName = user.FirstName;
                Email = user.Email;
                LastName = user.LastName;
                Id = user.Id;
                ProfilePhotoId = user.ProfilePhotoId;
                Login = user.UserName;
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }

        public Guid ProfilePhotoId { get; set; }

        public override string ToString()
        {
            return string.Format($"First Name: {FirstName}; Last Name: {LastName}, Email: {Email}");
        }
   }
}