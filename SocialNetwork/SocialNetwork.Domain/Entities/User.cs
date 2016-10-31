using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.Domain.Entities
{
    [Serializable]
    public class User : BaseEntity, IUser, IUser<Guid>
    {
        public User()
        {
        }

        public User(string fname, string lname, string email, string pass, ProfilePhoto profilePhoto)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            HashPassword = pass;
            ProfilePhotoId = profilePhoto.Id;
            ProfilePhoto = profilePhoto;
        }

        public User(string fname, string lname, string email, ProfilePhoto profilePhoto)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            ProfilePhotoId = profilePhoto.Id;
            ProfilePhoto = profilePhoto;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string HashPassword { get; set; }

        public DateTime Birthdate { get; set; }

        public Guid LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [Required]
        public Guid ProfilePhotoId { get; set; }

        [ForeignKey("ProfilePhotoId")]
        public virtual ProfilePhoto ProfilePhoto { get; set; }

        public string SecurityStamp { get; set; }

        public virtual List<Message> Messages { get; set; }

        public virtual List<Image> Images { get; set; }

        public virtual List<Role> Roles { get; set; }

        public override string ToString()
        {
            return string.Format($"First Name: {FirstName}; Last Name: {LastName}, Email: {Email}");
        }  
    }
}