using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace RChat.UserPerRelations
{
    public interface IUserPerRelationManager : IDomainService
    {
        IEnumerable<UserPerRelation> GetAll();
        Task<UserPerRelation> GetUserPerRelationById(int id);

        Task<UserPerRelation> CreateUserPerRelation(UserPerRelation entity);
        void UpdateUserPerRelation(UserPerRelation entity);
        void DeleteUserPerRelation(int id);
    }
}
