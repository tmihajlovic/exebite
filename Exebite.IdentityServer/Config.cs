using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Exebite.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<Client> GetClients(IConfiguration configuration) =>
            new Client[]
            {
                new Client {
                    ClientId = configuration["Apps:Exebite.ClientApp:ClientId"],
                    ClientName = configuration["Apps:Exebite.ClientApp:Name"],
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string> { "openid" },
                    RedirectUris = new List<string> { $"{configuration["Apps:Exebite.ClientApp:Url"]}/auth-callback" },
                    PostLogoutRedirectUris = new List<string> { configuration["Apps:Exebite.ClientApp:Url"] },
                    AllowedCorsOrigins = new List<string> { configuration["Apps:Exebite.ClientApp:Url"] },
                    AllowAccessTokensViaBrowser = true,
                }
            };
    }
}
