using Abp.Authorization;
using RChat.Authorization.Roles;
using RChat.Authorization.Users;

namespace RChat.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
