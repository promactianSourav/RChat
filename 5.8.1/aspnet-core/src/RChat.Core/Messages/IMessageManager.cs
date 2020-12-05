using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace RChat.Messages
{
    public interface IMessageManager : IDomainService
    {
        IEnumerable<Message> GetAllList();
        IEnumerable<Message> GetAllListForUnReadMessages(long userPerRelationId);
        void UpdateUnReadMessageToRead(int userPerRelationId);
        IEnumerable<Message> GetAllForBothUser(int userPerRelationOne,int userPerRelationTwo);
        Task<Message> GetMessageById(int id);

        Task<Message> CreateMessage(Message entity);
        Task<Message> UpdateMessage(Message entity);
        Task DeleteMessage(int id);
    }
}
