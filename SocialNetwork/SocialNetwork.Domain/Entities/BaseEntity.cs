using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Entities
{
    [Serializable]
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
    }
}