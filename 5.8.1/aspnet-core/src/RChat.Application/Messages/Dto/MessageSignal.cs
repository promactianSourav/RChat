using System;
using System.Collections.Generic;
using System.Text;

namespace RChat.Messages.Dto
{
    public class MessageSignal
    {
        public long? MessageReceiverId { get; set; }
        public long MessageUnReadCount { get; set; }
        public long? MessageCurrentUserPerRelationId { get; set; }
        public string MessageDescription { get; set; }
    }
}
