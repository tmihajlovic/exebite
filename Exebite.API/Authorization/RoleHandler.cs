using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Either;
using Exebite.Common;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public class RoleHandler : AuthorizationHandler<RequireRoleRequirment>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireRoleRequirment requirement)
        {
            new Right<Error, string>(context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value)
                .Map(r => Enum.GetName(typeof(RoleType), int.Parse(r)))
                .Map(x => CheckTheRole(x, requirement, context));
        }

        private bool CheckTheRole(string role, RequireRoleRequirment requirement, AuthorizationHandlerContext context)
        {
            if (requirement.Roles.Any(str => role.IndexOf(str, StringComparison.OrdinalIgnoreCase) > -1))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return true;
        }
    }
}
