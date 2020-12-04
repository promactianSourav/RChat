using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace RChat.UserPerRelations.Dto
{
    [AutoMapTo(typeof(UserPerRelation))]
    public class UpdateUserPerRelationInput
    {
        
        public int Id { get; set; }
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public DateTime TimeStatus { get; set; }
    }
}
