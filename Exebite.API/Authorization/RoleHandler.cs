using System.Threading.Tasks;
using Either;
using Exebite.Business;
using Exebite.Common;
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
            switch (await _roleService.GetRoleForGoogleUserAsync(context.User.Identity.Name).ConfigureAwait(false))
            {
                case Right<Error, string> role:
                    if (requirement.Roles.Contains(role))
                    {
                        context.Succeed(requirement);
                    }

                    break;
                default:
                    context.Fail();
                    break;
            }
        }
    }
}
