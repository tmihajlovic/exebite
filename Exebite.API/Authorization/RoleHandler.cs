using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public class RoleHandler : AuthorizationHandler<RequireRoleRequirment>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireRoleRequirment requirement)
        {
            var role = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
            CheckTheRole(role, context, requirement);
        }

        private void CheckTheRole(string role, AuthorizationHandlerContext context, RequireRoleRequirment requirement)
        {
            if (requirement.Roles.Any(req => req.Equals(role, StringComparison.InvariantCultureIgnoreCase)))
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
