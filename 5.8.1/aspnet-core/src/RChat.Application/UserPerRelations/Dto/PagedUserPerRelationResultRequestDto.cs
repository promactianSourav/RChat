using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace RChat.UserPerRelations.Dto
{
    public class PagedUserPerRelationResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
