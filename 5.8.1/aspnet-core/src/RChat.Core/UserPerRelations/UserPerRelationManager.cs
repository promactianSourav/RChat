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
            var userPerRelation = repositoryUserPerRelation.FirstOrDefault(x => x.Id == entity.Id);

            if(userPerRelation != null)
            {
                throw new UserFriendlyException("Already Exist.");
            }

            return await repositoryUserPerRelation.InsertAsync(entity);
        }

        public void DeleteUserPerRelation(int id)
        {
            var userPerRelation = repositoryUserPerRelation.FirstOrDefault(x => x.Id == id);
            if(userPerRelation == null)
            {
                throw new UserFriendlyException("No Data Found.");
            }
            else
            {
                repositoryUserPerRelation.Delete(userPerRelation);
            }
        }

        public  IEnumerable<UserPerRelation> GetAll()
        {
            return repositoryUserPerRelation.GetAllList();
            
        }



        public async Task<UserPerRelation> GetUserPerRelationById(int id)
        {
            return await repositoryUserPerRelation.GetAsync(id);
        }

        public void UpdateUserPerRelation(UserPerRelation entity)
        {
            repositoryUserPerRelation.Update(entity);
        }
    }
}
