using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;

namespace RChat.Messages
{
    public class MessageManager : DomainService, IMessageManager
    {
        private readonly IRepository<Message> repositoryMessage;

        public MessageManager(IRepository<Message> repositoryMessage)
        {
            this.repositoryMessage = repositoryMessage;
        }


        public async Task<Message> CreateMessage(Message entity)
        {
            var message = repositoryMessage.FirstOrDefault(x => x.Id == entity.Id);

            if (message != null)
            {
                throw new UserFriendlyException("Already Exist.");
            }

            return await repositoryMessage.InsertAsync(entity);
        }

        public Task DeleteMessage(int id)
        {
            var message = repositoryMessage.FirstOrDefault(x => x.Id == id);
            if (message == null)
            {
                throw new UserFriendlyException("No Data Found.");
            }
            else
            {
                return repositoryMessage.DeleteAsync(message);
            }
        }

        public IEnumerable<Message> GetAllList()
        {
            return repositoryMessage.GetAllList();

        }



        public async Task<Message> GetMessageById(int id)
        {
            return await repositoryMessage.GetAsync(id);
        }

        public async Task<Message> UpdateMessage(Message entity)
        {
            return await repositoryMessage.UpdateAsync(entity);
        }
    }
}
