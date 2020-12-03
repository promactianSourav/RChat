using System.Threading.Tasks;
using Abp.Application.Services;
using RChat.Authorization.Accounts.Dto;

namespace RChat.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
