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
        Task<UserPerRelation> GetUserPerRelationForSenderAndReceiver(int senderId,int receiverId);

        Task<UserPerRelation> CreateUserPerRelation(UserPerRelation entity);
        Task<UserPerRelation> UpdateUserPerRelation(UserPerRelation entity);
        Task DeleteUserPerRelation(int id);
    }
}
