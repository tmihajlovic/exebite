using Exebite.IdentityServer.Filters;
using Exebite.IdentityServer.Model;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exebite.IdentityServer.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IEventService events)
        {
            _interaction = interaction;
            _events = events;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var scheme = GoogleDefaults.AuthenticationScheme;

            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (!Url.IsLocalUrl(returnUrl) && !_interaction.IsValidReturnUrl(returnUrl))
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            // start challenge and roundtrip the return URL and scheme 
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(LoginCallback)),
                Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", scheme },
                },
            };

            return Challenge(props, scheme);
        }

        [HttpGet]
        public async Task<IActionResult> LoginCallback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            var externalUser = result.Principal;
            var provider = result.Properties.Items["scheme"];
            var providerUserId = externalUser.Claims.First(claim => claim.Type == JwtClaimTypes.Id).Value;
            var subjectId = externalUser.Claims.First(claim => claim.Type == JwtClaimTypes.Email).Value;
            var name = externalUser.Claims.First(claim => claim.Type == JwtClaimTypes.Name).Value;

            // this allows us to collect any additional claims or properties
            // for the specific protocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            // also, these claims are used in ProfileService.
            var additionalLocalClaims = new List<Claim>();

            foreach (var claim in result.Principal.Claims)
            {
                additionalLocalClaims.Add(claim);
            }

            var localSignInProps = new AuthenticationProperties();

            // issue authentication cookie for user
            var isuser = new IdentityServerUser(subjectId)
            {
                DisplayName = name,
                IdentityProvider = provider,
                AdditionalClaims = additionalLocalClaims,
            };

            await HttpContext.SignInAsync(isuser, localSignInProps);

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // retrieve return URL
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            // check if external login is in the context of an OIDC request
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, subjectId, name, true, context?.Client.ClientId));

            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId) => await Logout(new LogoutInputModel { LogoutId = logoutId });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            var logout = await _interaction.GetLogoutContextAsync(model.LogoutId);

            var postLogoutRedirectUri = logout?.PostLogoutRedirectUri;

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            if (string.IsNullOrEmpty(postLogoutRedirectUri)) postLogoutRedirectUri = "~/";

            return Redirect(postLogoutRedirectUri);
        }
    }
}
