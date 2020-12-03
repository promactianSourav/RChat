using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using RChat.Authorization.Users;

namespace RChat.UserPerRelations
{
    public class UserPerRelation : FullAuditedEntity
    {
      
        [ForeignKey("Sender")]
        public long? SenderId { get; set; }
        public virtual User Sender { get; set; }

      
        [ForeignKey("Receiver")]
        public long? ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        [Required]
        public DateTime TimeStatus { get; set; }
    }
}
