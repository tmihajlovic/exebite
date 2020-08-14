using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public class RequirePermissionRequirement : IAuthorizationRequirement
    {
        public RequirePermissionRequirement(List<string> permissions)
        {
            Permissions = permissions;
        }

        public List<string> Permissions { get; }
    }
}
