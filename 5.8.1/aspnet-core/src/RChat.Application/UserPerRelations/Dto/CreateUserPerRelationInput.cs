using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace RChat.UserPerRelations.Dto
{
    [AutoMapTo(typeof(UserPerRelation))]
    public class CreateUserPerRelationInput
    {
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public DateTime TimeStatus { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
