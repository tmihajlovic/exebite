using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public class RequireRoleRequirment : IAuthorizationRequirement
    {
        public RequireRoleRequirment(List<string> roles)
        {
            Roles = roles;
        }

        public List<string> Roles { get; }
    }
}
