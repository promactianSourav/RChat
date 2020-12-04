using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using RChat.Messages.Dto;

namespace RChat.Messages
{
    public interface IMessageAppService : IApplicationService
    {
        IEnumerable<GetMessageOutput> ListAll();
        IEnumerable<GetMessageOutput> GetAllForBothUser(int userPerRelationOne, int userPerRelationTwo);
        
    }
}
