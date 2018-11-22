using System;
using System.Linq;
using System.Threading.Tasks;
using Either;
using Exebite.Business;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public class RoleHandler : AuthorizationHandler<RequireRoleRequirment>
    {
        private readonly IRoleService _roleService;

        public RoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireRoleRequirment requirement)
        {
            var role = await _roleService.GetRoleForGoogleUserAsync(context.User.Claims).ConfigureAwait(false);
            role.Map(x => CheckTheRole(x, requirement, context))
                .Reduce(_ => FailTheContext(context));
        }

        private bool FailTheContext(AuthorizationHandlerContext context)
        {
            context.Fail();
            return true;
        }

        private bool CheckTheRole(string role, RequireRoleRequirment requirement, AuthorizationHandlerContext context)
        {
            if (requirement.Roles.Any(str => role.IndexOf(str, StringComparison.OrdinalIgnoreCase) > -1))
            {
                context.Succeed(requirement);
            }

            return true;
        }
    }
}
