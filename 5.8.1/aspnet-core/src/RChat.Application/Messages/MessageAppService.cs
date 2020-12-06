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
using Microsoft.AspNetCore.SignalR;
using RChat.Messages.Dto;
using RChat.UserPerRelations;

namespace RChat.Messages
{
    [AbpAuthorize]
    public class MessageAppService : AsyncCrudAppService<Message, MessageDto, int, PagedMessageResultRequestDto, CreateMessageInput, MessageDto>, IMessageAppService
    {
        private readonly IMessageManager messageManager;
        private readonly IMapper mapper;
        private readonly IHubContext<ChatHub> hubContext;
        private readonly IUserPerRelationManager userPerRelationManager;

        public MessageAppService(IRepository<Message> repository, IMessageManager messageManager, IMapper mapper,IHubContext<ChatHub> hubContext,IUserPerRelationManager userPerRelationManager) : base(repository)
        {
            this.messageManager = messageManager;
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.userPerRelationManager = userPerRelationManager;
        }

        public override async Task<MessageDto> CreateAsync(CreateMessageInput input)
        {
            var entity = mapper.Map<CreateMessageInput, Message>(input);
            var output = await messageManager.CreateMessage(entity);

            
            //int v2 = output.UserPerRelationId == null ? 1:output.UserPerRelationId;
            int v2 = output.UserPerRelationId ?? 0;
          

            if (output != null)
            {
                MessageSignal msg = new MessageSignal();
                
                msg.MessageReceiverId =  userPerRelationManager.GetSingleUserPerRelation(v2).SenderId;
                
                msg.MessageUnReadCount = messageManager.GetAllListForUnReadMessages(v2).ToList().Count;
                msg.MessageId = output.Id;
                long r = userPerRelationManager.GetSingleUserPerRelation(v2).SenderId ?? 0;
                long s = userPerRelationManager.GetSingleUserPerRelation(v2).ReceiverId ?? 0;
                msg.MessageCurrentUserPerRelationId = userPerRelationManager.GetUserPerRelationForSenderAndReceiverForMessage(s, r).Id;
                msg.MessageDescription = output.MessageContent;
                await hubContext.Clients.All.SendAsync("checkMessage", msg);
            }
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

        public IEnumerable<GetMessageOutput> GetAllForBothUser(int userPerRelationOne, int userPerRelationTwo)
        {
            var getAll = messageManager.GetAllForBothUser(userPerRelationOne, userPerRelationTwo).ToList();
            var output = mapper.Map<List<Message>, List<GetMessageOutput>>(getAll);
            return output;
        }

        public void UpdateUnReadMessageToRead(int userPerRelationId)
        {
            messageManager.UpdateUnReadMessageToRead(userPerRelationId);
        }

        public void UpdateSingleUnReadMessageToRead(int messageId)
        {
            messageManager.UpdateSingleUnReadMessageToRead(messageId);
        }
    }
}

