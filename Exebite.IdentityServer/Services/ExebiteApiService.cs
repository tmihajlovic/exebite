using Exebite.IdentityServer.Authorization;
using Exebite.IdentityServer.Interfaces;
using Exebite.IdentityServer.Model;
using IdentityModel;
using IdentityServer4;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Exebite.IdentityServer.Services
{
    public class ExebiteApiService : IExebiteApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IdentityServerTools _identityServerTools;
        private readonly IConfiguration _configuration;
        private const int tokenLifeTime = 120;

        public ExebiteApiService(HttpClient httpClient, IdentityServerTools identityServerTools, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _identityServerTools = identityServerTools;
            _configuration = configuration;
        }

        public async Task<UserInfo> GetUserInfo(string id)
        {
            var accessToken = await _identityServerTools.IssueJwtAsync(
                tokenLifeTime,
                new Claim[]
                {
                    new Claim("Permission", Permissions.UserInfoRead),
                    new Claim(JwtClaimTypes.Audience, _configuration["Apps:Exebite.API:Name"])
                });

            HttpResponseMessage response = null;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"userinfo/{id}"))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                response = await _httpClient.SendAsync(requestMessage);
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var userInfo = JsonSerializer.Deserialize<UserInfo>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return userInfo;
            }

            return null;
        }
    }
}
