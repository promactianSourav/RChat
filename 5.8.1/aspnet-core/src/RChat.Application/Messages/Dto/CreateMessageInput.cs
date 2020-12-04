using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace RChat.Messages.Dto
{
    [AutoMapTo(typeof(Message))]
    public class CreateMessageInput
    {
        public string MessageContent { get; set; }
        public int? UserPerRelationId { get; set; }
        public Boolean IsRead { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
