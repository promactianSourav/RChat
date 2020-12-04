using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace RChat.Messages.Dto
{
    [AutoMap(typeof(Message))]
    public class MessageDto : EntityDto<int>
    {
        public string MessageContent { get; set; }
        public int? UserPerRelationId { get; set; }
        public Boolean IsRead { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
