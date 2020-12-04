using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using RChat.UserPerRelations;

namespace RChat.Messages
{
    public class Message : FullAuditedEntity
    {
        public string MessageContent { get; set; }

        [Required]
        [ForeignKey("UserPerRelation")]
        public int? UserPerRelationId { get; set; }
        public virtual UserPerRelation UserPerRelation { get; set; }

        public Boolean IsRead { get; set; }
        //public DateTime CreationTime { get; set; }
    }
}
