using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace RChat.Controllers
{
    public abstract class RChatControllerBase: AbpController
    {
        protected RChatControllerBase()
        {
            LocalizationSourceName = RChatConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
