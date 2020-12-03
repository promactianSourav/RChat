using Abp.Application.Services;
using RChat.MultiTenancy.Dto;

namespace RChat.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

