using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace RChat.Messages.Dto
{
    public class PagedMessageResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
