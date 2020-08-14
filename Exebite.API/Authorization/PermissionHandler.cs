using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public class PermissionHandler : AuthorizationHandler<RequirePermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RequirePermissionRequirement requirement)
        {
            var permission = context.User.Claims.FirstOrDefault(claim => claim.Type == "Permission")?.Value;
            CheckThePermission(permission, context, requirement);
        }

        private void CheckThePermission(string permission, AuthorizationHandlerContext context, RequirePermissionRequirement requirement)
        {
            if (requirement.Permissions.Any(req => req.Equals(permission, StringComparison.InvariantCultureIgnoreCase)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
