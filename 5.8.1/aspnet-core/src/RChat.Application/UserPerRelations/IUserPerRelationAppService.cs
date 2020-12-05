using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using RChat.UserPerRelations.Dto;

namespace RChat.UserPerRelations
{
    public interface IUserPerRelationAppService : IApplicationService
    {
        IEnumerable<GetUserPerRelationOutput> ListAll();
        GetUserPerRelationOutput GetSingleUserPerRelation(int id);
        Task<GetUserPerRelationOutput> GetUserPerRelationForSenderAndReceiver(long senderId,long receiverId);
    }
}
