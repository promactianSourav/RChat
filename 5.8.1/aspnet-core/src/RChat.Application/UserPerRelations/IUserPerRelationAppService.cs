using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using RChat.UserPerRelations.Dto;

namespace RChat.UserPerRelations
{
    public interface IUserPerRelationAppService : IApplicationService
    {
        IEnumerable<GetUserPerRelationOutput> ListAll();
    }
}
