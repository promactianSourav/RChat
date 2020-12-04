using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace RChat.UserPerRelations.Dto
{
    [AutoMapFrom(typeof(UserPerRelation))]
    public class GetUserPerRelationOutput
    {
        public int Id { get; set; }
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public DateTime TimeStatus { get; set; }
    }
}
