using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace RChat.UserPerRelations
{
    public interface IUserPerRelationManager : IDomainService
    {
        IEnumerable<UserPerRelation> GetAllList();
        Task<UserPerRelation> GetUserPerRelationById(int id);
        UserPerRelation GetSingleUserPerRelation(int id);
        Task<UserPerRelation> GetUserPerRelationForSenderAndReceiver(long senderId,long receiverId);

        Task<UserPerRelation> CreateUserPerRelation(UserPerRelation entity);
        Task<UserPerRelation> UpdateUserPerRelation(UserPerRelation entity);
        Task DeleteUserPerRelation(int id);
    }
}
