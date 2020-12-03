using System.Threading.Tasks;
using Abp.Application.Services;
using RChat.Sessions.Dto;

namespace RChat.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
