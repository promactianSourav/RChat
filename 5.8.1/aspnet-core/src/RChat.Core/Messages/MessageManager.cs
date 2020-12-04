using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Timing;
using Abp.UI;
using RChat.UserPerRelations;

namespace RChat.Messages
{
    public class MessageManager : DomainService, IMessageManager
    {
        private readonly IRepository<Message> repositoryMessage;
        private readonly IRepository<UserPerRelation> repositoryUserPerRelation;

        public MessageManager(IRepository<Message> repositoryMessage,IRepository<UserPerRelation> repositoryUserPerRelation)
        {
            this.repositoryMessage = repositoryMessage;
            this.repositoryUserPerRelation = repositoryUserPerRelation;
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

        public IEnumerable<Message> GetAllForBothUser(int userPerRelationOne, int userPerRelationTwo)
        {
            var messages = repositoryMessage.GetAllList().Where(x => (x.UserPerRelationId == userPerRelationOne || x.UserPerRelationId == userPerRelationTwo)).OrderBy(x => x.CreationTime);
            return messages;
            //var userPerRelationSenderExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId));
            //var userPerRelationReceiverExist = repositoryUserPerRelation.FirstOrDefault(x =>(x.SenderId == receiverId && x.ReceiverId == senderId));
            //Console.WriteLine(userPerRelationSenderExist+" sender");
            //Console.WriteLine(userPerRelationReceiverExist+" receiver");
            //if (userPerRelationSenderExist == null && userPerRelationReceiverExist == null)
            //{
            //    UserPerRelation userPerRelation = new UserPerRelation();
            //    userPerRelation.SenderId = senderId;
            //    userPerRelation.ReceiverId = receiverId;
            //    userPerRelation.TimeStatus = Clock.Now;
            //    repositoryUserPerRelation.Insert(userPerRelation);

            //    UserPerRelation userPerRelationReceiver = new UserPerRelation();
            //    userPerRelationReceiver.SenderId = receiverId;
            //    userPerRelationReceiver.ReceiverId = senderId;
            //    userPerRelationReceiver.TimeStatus = Clock.Now;
            //    repositoryUserPerRelation.Insert(userPerRelationReceiver);


            //    userPerRelationSenderExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId));
            //    userPerRelationReceiverExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == receiverId && x.ReceiverId == senderId));

            //    Console.WriteLine(userPerRelationSenderExist + " sender!");
            //    Console.WriteLine(userPerRelationReceiverExist + " receiver!");

            //    var messages = repositoryMessage.GetAllList().Where(x => (x.UserPerRelationId == userPerRelationSenderExist.Id || x.UserPerRelationId == userPerRelationReceiverExist.Id)).OrderBy(x => x.CreationTime);
            //    return messages;
            //    //userPerRelationExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId));

            //}
            //else if (userPerRelationSenderExist != null && userPerRelationReceiverExist == null)
            //{
            //    //UserPerRelation userPerRelation = new UserPerRelation();
            //    //userPerRelation.SenderId = senderId;
            //    //userPerRelation.ReceiverId = receiverId;
            //    //userPerRelation.TimeStatus = Clock.Now;
            //    //repositoryUserPerRelation.Insert(userPerRelation);

            //    UserPerRelation userPerRelationReceiver = new UserPerRelation();
            //    userPerRelationReceiver.SenderId = receiverId;
            //    userPerRelationReceiver.ReceiverId = senderId;
            //    userPerRelationReceiver.TimeStatus = Clock.Now;
            //    repositoryUserPerRelation.Insert(userPerRelationReceiver);

            //    userPerRelationSenderExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId));
            //    userPerRelationReceiverExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == receiverId && x.ReceiverId == senderId));

            //    Console.WriteLine(userPerRelationSenderExist + " sender!");
            //    Console.WriteLine(userPerRelationReceiverExist + " receiver");

            //    var messages = repositoryMessage.GetAllList().Where(x => (x.UserPerRelationId == userPerRelationSenderExist.Id || x.UserPerRelationId == userPerRelationReceiverExist.Id)).OrderBy(x => x.CreationTime);
            //    return messages;

            //    //userPerRelationExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId));

            //}
            //else if (userPerRelationSenderExist == null && userPerRelationReceiverExist != null)
            //{
            //    UserPerRelation userPerRelation = new UserPerRelation();
            //    userPerRelation.SenderId = senderId;
            //    userPerRelation.ReceiverId = receiverId;
            //    userPerRelation.TimeStatus = Clock.Now;
            //    repositoryUserPerRelation.Insert(userPerRelation);

            //    userPerRelationSenderExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId));
            //    userPerRelationReceiverExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == receiverId && x.ReceiverId == senderId));

            //    Console.WriteLine(userPerRelationSenderExist + " sender");
            //    Console.WriteLine(userPerRelationReceiverExist + " receiver!");

            //    var messages = repositoryMessage.GetAllList().Where(x => (x.UserPerRelationId == userPerRelationSenderExist.Id || x.UserPerRelationId == userPerRelationReceiverExist.Id)).OrderBy(x => x.CreationTime);
            //    return messages;

            //    //UserPerRelation userPerRelationReceiver = new UserPerRelation();
            //    //userPerRelationReceiver.SenderId = receiverId;
            //    //userPerRelationReceiver.ReceiverId = senderId;
            //    //userPerRelationReceiver.TimeStatus = Clock.Now;
            //    //repositoryUserPerRelation.Insert(userPerRelationReceiver);

            //    //userPerRelationExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId));

            //}
            //else
            //{
            //    userPerRelationSenderExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == senderId && x.ReceiverId == receiverId));
            //    userPerRelationReceiverExist = repositoryUserPerRelation.FirstOrDefault(x => (x.SenderId == receiverId && x.ReceiverId == senderId));

            //    Console.WriteLine(userPerRelationSenderExist + " sender");
            //    Console.WriteLine(userPerRelationReceiverExist + " receiver");

            //    var messages = repositoryMessage.GetAllList().Where(x => (x.UserPerRelationId == userPerRelationSenderExist.Id || x.UserPerRelationId == userPerRelationReceiverExist.Id)).OrderBy(x=>x.CreationTime);
            //    return messages;
            //}



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
