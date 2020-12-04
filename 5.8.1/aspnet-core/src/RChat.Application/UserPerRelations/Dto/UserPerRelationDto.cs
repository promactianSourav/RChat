using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace RChat.UserPerRelations.Dto
{
    [AutoMap(typeof(UserPerRelation))]
    public class UserPerRelationDto : EntityDto<int>
    {
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public DateTime TimeStatus { get; set; }
    }
}
