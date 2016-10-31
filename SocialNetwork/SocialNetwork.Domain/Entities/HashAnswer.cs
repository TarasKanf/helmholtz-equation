using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class HashAnswer : BaseEntity
    {
        public HashAnswer()
        {
        }

        [Required]
        public Guid HandlerTypeId { get; set; }

        [ForeignKey("HandlerTypeId")]
        public virtual HandlerType Handler { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
