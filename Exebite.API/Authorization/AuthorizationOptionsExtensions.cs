using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Exebite.API.Authorization
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddCustomPolicies(this AuthorizationOptions options)
        {
            AddRestaurantPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Desk },
                readAccess: new List<string> { Roles.Admin, Roles.Desk, Roles.User },
                updateAccess: new List<string> { Roles.Admin, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Desk });

            AddCustomerAliasesPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Desk },
                readAccess: new List<string> { Roles.Admin, Roles.Desk },
                updateAccess: new List<string> { Roles.Admin, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Desk });

            AddCustomerPolicies(
                options,
                createAccess: new List<string> { Roles.Admin },
                readAccess: new List<string> { Roles.Admin, Roles.Desk, Roles.User },
                updateAccess: new List<string> { Roles.Admin, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin });

            AddDailyMenuPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Restaurant },
                readAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk, Roles.User },
                updateAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Restaurant });

            AddFoodPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Restaurant },
                readAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk },
                updateAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Restaurant });

            AddLocationPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Desk },
                readAccess: new List<string> { Roles.Admin, Roles.Desk },
                updateAccess: new List<string> { Roles.Admin, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Desk });

            AddMealPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Restaurant },
                readAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk },
                updateAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Restaurant });

            AddOrdersPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Desk, Roles.User },
                readAccess: new List<string> { Roles.Admin, Roles.Desk, Roles.User, Roles.Restaurant },
                updateAccess: new List<string> { Roles.Admin, Roles.Desk, Roles.User },
                deleteAccess: new List<string> { Roles.Admin, Roles.Desk, Roles.User });

            AddPaymentPolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Desk },
                readAccess: new List<string> { Roles.Admin, Roles.Desk, Roles.User },
                updateAccess: new List<string> { Roles.Admin, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Desk });

            AddRecipePolicies(
                options,
                createAccess: new List<string> { Roles.Admin, Roles.Restaurant },
                readAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk },
                updateAccess: new List<string> { Roles.Admin, Roles.Restaurant, Roles.Desk },
                deleteAccess: new List<string> { Roles.Admin, Roles.Restaurant });

            AddRolePolicies(
                options,
                createAccess: new List<string> { Roles.Admin },
                readAccess: new List<string> { Roles.Admin },
                updateAccess: new List<string> { Roles.Admin },
                deleteAccess: new List<string> { Roles.Admin });

            AddUserInfoPolicies(
                options,
                readAccess: new List<string> { Permissions.UserInfoRead });
        }

        private static void AddRestaurantPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateRestaurantAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadRestaurantAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateRestaurantAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteRestaurantAccessPolicy, deleteAccess);
        }

        private static void AddCustomerAliasesPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateCustomerAliasesAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadCustomerAliasesAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateCustomerAliasesAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteCustomerAliasesAccessPolicy, deleteAccess);
        }

        private static void AddCustomerPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateCustomerAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadCustomerAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateCustomerAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteCustomerAccessPolicy, deleteAccess);
        }

        private static void AddDailyMenuPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateDailyMenuAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadDailyMenuAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateDailyMenuAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteDailyMenuAccessPolicy, deleteAccess);
        }

        private static void AddFoodPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateFoodAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadFoodAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateFoodAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteFoodAccessPolicy, deleteAccess);
        }

        private static void AddLocationPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateLocationAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadLocationAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateLocationAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteLocationAccessPolicy, deleteAccess);
        }

        private static void AddMealPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateMealAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadMealAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateMealAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteMealAccessPolicy, deleteAccess);
        }

        private static void AddOrdersPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateOrdersAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadOrdersAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateOrdersAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteOrdersAccessPolicy, deleteAccess);
        }

        private static void AddPaymentPolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreatePaymentAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadPaymentAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdatePaymentAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeletePaymentAccessPolicy, deleteAccess);
        }

        private static void AddRecipePolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateRecipeAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadRecipeAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateRecipeAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteRecipeAccessPolicy, deleteAccess);
        }

        private static void AddRolePolicies(
            AuthorizationOptions options,
            List<string> createAccess,
            List<string> readAccess,
            List<string> updateAccess,
            List<string> deleteAccess)
        {
            AddPolicy(options, AccessPolicy.CreateRoleAccessPolicy, createAccess);
            AddPolicy(options, AccessPolicy.ReadRoleAccessPolicy, readAccess);
            AddPolicy(options, AccessPolicy.UpdateRoleAccessPolicy, updateAccess);
            AddPolicy(options, AccessPolicy.DeleteRoleAccessPolicy, deleteAccess);
        }

        private static void AddPolicy(AuthorizationOptions options, string policyName, List<string> access)
        {
            options.AddPolicy(policyName, policy => policy.AddRequirements(new RequireRoleRequirment(access)));
        }

        private static void AddUserInfoPolicies(
            AuthorizationOptions options,
            List<string> readAccess)
        {
            AddPermissionPolicy(options, AccessPolicy.ReadUserInfoAccessPolicy, readAccess);
        }

        private static void AddPermissionPolicy(AuthorizationOptions options, string policyName, List<string> access)
        {
            options.AddPolicy(policyName, policy => policy.AddRequirements(new RequirePermissionRequirement(access)));
        }
    }
}
