using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace RChat.Messages
{
    public interface IMessageManager : IDomainService
    {
        IEnumerable<Message> GetAll();
        Task<Message> GetMessageById(int id);

        Task<Message> CreateMessage(Message entity);
        void UpdateMessage(Message entity);
        void DeleteMessage(int id);
    }
}
