using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RChat.UserPerRelations.Dto;

namespace RChat.UserPerRelations
{
    [AbpAuthorize]
    public class UserPerRelationAppService : AsyncCrudAppService<UserPerRelation, UserPerRelationDto, int, PagedUserPerRelationResultRequestDto, CreateUserPerRelationInput,UserPerRelationDto >,IUserPerRelationAppService
    {
        private readonly IUserPerRelationManager userPerRelationManager;
        private readonly IMapper mapper;

        public UserPerRelationAppService(IRepository<UserPerRelation> repository,IUserPerRelationManager userPerRelationManager,IMapper mapper) : base(repository)
        {
            this.userPerRelationManager = userPerRelationManager;
            this.mapper = mapper;
        }

        public override async Task<UserPerRelationDto> CreateAsync(CreateUserPerRelationInput input)
        {
            var entity = mapper.Map<CreateUserPerRelationInput, UserPerRelation>(input);
            var output = await userPerRelationManager.CreateUserPerRelation(entity);
            var returnDto = mapper.Map<UserPerRelation, UserPerRelationDto>(output);
            return returnDto;
        }

        public override Task<UserPerRelationDto> UpdateAsync(UserPerRelationDto input)
        {
            var entity = mapper.Map<UserPerRelationDto, UserPerRelation>(input);
            var output =  userPerRelationManager.UpdateUserPerRelation(entity);
            var returnDto = mapper.Map<Task<UserPerRelation>, Task<UserPerRelationDto>>(output);
            return returnDto;
            //return await base.UpdateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {

            await userPerRelationManager.DeleteUserPerRelation(input.Id);
        }


        protected override Task<UserPerRelation> GetEntityByIdAsync(int id)
        {
            
            var output = userPerRelationManager.GetUserPerRelationById(id);
            //var returnDto = mapper.Map<Task<UserPerRelation>, Task<UserPerRelationDto>>(output);
            return output;
            //return base.GetEntityByIdAsync(id);
        }
        public IEnumerable<GetUserPerRelationOutput> ListAll()
        {
            var getAll = userPerRelationManager.GetAllList().ToList();
            var output = mapper.Map<List<UserPerRelation>, List<GetUserPerRelationOutput>>(getAll);
            return output;
        }

        //public async Task<UserPerRelationDto> CreateAsync(UserPerRelationDto input)
        //{
        //    var entity = mapper.Map<UserPerRelationDto, UserPerRelation>(input);
        //    var output = await userPerRelationManager.CreateUserPerRelation(entity);
        //    var returnDto = mapper.Map<UserPerRelation, UserPerRelationDto>(output);
        //    return returnDto;
        //}

        //public Task<PagedResultDto<UserPerRelationDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        //{
        //    throw new NotImplementedException();
        //}

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetUserPerRelationOutput> GetUserPerRelationForSenderAndReceiver(int senderId, int receiverId)
        {
            var getUserPerRelation = await userPerRelationManager.GetUserPerRelationForSenderAndReceiver(senderId, receiverId);
            var output = mapper.Map<UserPerRelation,GetUserPerRelationOutput>(getUserPerRelation);
            return output;
        }
    }
}
