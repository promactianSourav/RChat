using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;

namespace RChat.UserPerRelations
{
    public class UserPerRelationManager : DomainService, IUserPerRelationManager
    {
        private readonly IRepository<UserPerRelation> repositoryUserPerRelation;

        public UserPerRelationManager(IRepository<UserPerRelation> repositoryUserPerRelation)
        {
            this.repositoryUserPerRelation = repositoryUserPerRelation;
        }


        public async Task<UserPerRelation> CreateUserPerRelation(UserPerRelation entity)
        {
            var userPerRelation = repositoryUserPerRelation.FirstOrDefault(x => x.ReceiverId == entity.ReceiverId && x.SenderId == entity.SenderId && x.IsDeleted==false);

            if(userPerRelation != null)
            {
                //throw new UserFriendlyException("Already Exist.");
                return await GetUserPerRelationById(userPerRelation.Id);
            }

            return await repositoryUserPerRelation.InsertAsync(entity);
        }

        public Task DeleteUserPerRelation(int id)
        {
            var userPerRelation = repositoryUserPerRelation.FirstOrDefault(x => x.Id == id && x.IsDeleted==false);
            if(userPerRelation == null)
            {
                throw new UserFriendlyException("No Data Found.");
            }
            else
            {
               return repositoryUserPerRelation.DeleteAsync(userPerRelation);
            }
        }

        public  IEnumerable<UserPerRelation> GetAllList()
        {
            return repositoryUserPerRelation.GetAllList();
            
        }

        public UserPerRelation GetSingleUserPerRelation(int id)
        {
            return repositoryUserPerRelation.Get(id);
        }

        public async Task<UserPerRelation> GetUserPerRelationById(int id)
        {
            return await repositoryUserPerRelation.GetAsync(id);
        }

        public async Task<UserPerRelation> GetUserPerRelationForSenderAndReceiver(long senderId, long receiverId)
        {
            return await repositoryUserPerRelation.FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId && x.IsDeleted==false);
        }
        public UserPerRelation GetUserPerRelationForSenderAndReceiverForMessage(long senderId, long receiverId)
        {
            return repositoryUserPerRelation.FirstOrDefault(x => x.SenderId == senderId && x.ReceiverId == receiverId && x.IsDeleted == false);
        }

        public async Task<UserPerRelation> UpdateUserPerRelation(UserPerRelation entity)
        {
           return await repositoryUserPerRelation.UpdateAsync(entity);
        }
    }
}
