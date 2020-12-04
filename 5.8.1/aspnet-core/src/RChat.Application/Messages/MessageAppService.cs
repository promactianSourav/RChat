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
using RChat.Messages.Dto;

namespace RChat.Messages
{
    [AbpAuthorize]
    public class MessageAppService : AsyncCrudAppService<Message, MessageDto, int, PagedMessageResultRequestDto, CreateMessageInput, MessageDto>, IMessageAppService
    {
        private readonly IMessageManager messageManager;
        private readonly IMapper mapper;

        public MessageAppService(IRepository<Message> repository, IMessageManager messageManager, IMapper mapper) : base(repository)
        {
            this.messageManager = messageManager;
            this.mapper = mapper;
        }

        public override async Task<MessageDto> CreateAsync(CreateMessageInput input)
        {
            var entity = mapper.Map<CreateMessageInput, Message>(input);
            var output = await messageManager.CreateMessage(entity);
            var returnDto = mapper.Map<Message, MessageDto>(output);
            return returnDto;
        }

        public override Task<MessageDto> UpdateAsync(MessageDto input)
        {
            var entity = mapper.Map<MessageDto, Message>(input);
            var output = messageManager.UpdateMessage(entity);
            var returnDto = mapper.Map<Task<Message>, Task<MessageDto>>(output);
            return returnDto;
            //return await base.UpdateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {

            await messageManager.DeleteMessage(input.Id);
        }


        protected override Task<Message> GetEntityByIdAsync(int id)
        {

            var output = messageManager.GetMessageById(id);
            //var returnDto = mapper.Map<Task<UserPerRelation>, Task<MessageDto>>(output);
            return output;
            //return base.GetEntityByIdAsync(id);
        }
        public IEnumerable<GetMessageOutput> ListAll()
        {
            var getAll = messageManager.GetAllList().ToList();
            var output = mapper.Map<List<Message>, List<GetMessageOutput>>(getAll);
            return output;
        }

        //public async Task<MessageDto> CreateAsync(MessageDto input)
        //{
        //    var entity = mapper.Map<MessageDto, UserPerRelation>(input);
        //    var output = await userPerRelationManager.CreateUserPerRelation(entity);
        //    var returnDto = mapper.Map<UserPerRelation, MessageDto>(output);
        //    return returnDto;
        //}

        //public Task<PagedResultDto<MessageDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        //{
        //    throw new NotImplementedException();
        //}

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

