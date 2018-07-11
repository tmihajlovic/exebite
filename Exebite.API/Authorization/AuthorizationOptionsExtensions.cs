using System.Collections.Generic;
using Exebite.API.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddCustomPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(
                AccessPolicy.WriteRestaurantAccessPolicy,
                policy => policy.AddRequirements(new RequireRoleRequirment(
                                                        new List<string>
                                                        {
                                                            Roles.Admin,
                                                            Roles.Desk
                                                        })));
            options.AddPolicy(
                AccessPolicy.WriteUsersAccessPolicy,
                policy => policy.AddRequirements(new RequireRoleRequirment(
                                                        new List<string>
                                                        {
                                                            Roles.Admin,
                                                            Roles.Desk
                                                        })));
            options.AddPolicy(
                AccessPolicy.ReadOrderAccessPolicy,
                policy => policy.AddRequirements(new RequireRoleRequirment(
                                                        new List<string>
                                                        {
                                                            Roles.Admin,
                                                            Roles.Desk,
                                                            Roles.Restaurant,
                                                            Roles.User
                                                        })));
            options.AddPolicy(
                AccessPolicy.WriteOrderAccessPolicy,
                policy => policy.AddRequirements(new RequireRoleRequirment(
                                                        new List<string>
                                                        {
                                                            Roles.Admin,
                                                            Roles.Desk,
                                                            Roles.User
                                                        })));
        }
    }
}
