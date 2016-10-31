using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Domain.Entities
{
    [Serializable]
    public class Connection : BaseEntity
    {
        public Connection()
        {
        }

        public Connection(Guid user1Id, Guid user2Id)
        {
            User1Id = user1Id;
            User2Id = user2Id;
        }

        [Required]
        public Guid User1Id { get; set; }

        [Required]
        public Guid User2Id { get; set; }

        [ForeignKey("User1Id")]
        public virtual User User1 { get; set; }

        [ForeignKey("User2Id")]
        public virtual User User2 { get; set; }      
    }
}