using Exebite.IdentityServer.Interfaces;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exebite.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IExebiteApiService _exebiteApiServices;

        public ProfileService(IExebiteApiService exebiteApiServices)
        {
            _exebiteApiServices = exebiteApiServices;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var emailClaim = context.Subject.Claims.FirstOrDefault(claim => claim.Type == JwtClaimTypes.Email);

            if (emailClaim != null)
            {
                context.IssuedClaims.Add(emailClaim);
                context.IssuedClaims.Add(context.Subject.Claims.FirstOrDefault(claim => claim.Type == "picture"));

                var userInfo = await _exebiteApiServices.GetUserInfo(emailClaim.Value);

                if (userInfo != null)
                {
                    context.IssuedClaims.Add(new Claim("role", userInfo.Role));
                }
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(true);
        }
    }
}
