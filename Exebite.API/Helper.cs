using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Exebite.API
{
    public static class Helper
    {
        public static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(
                        HttpStatusCode statusCode,
                        Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) =>
                            context =>
                            {
                                if (context.Request.Path.StartsWithSegments("/api"))
                                {
                                    context.Response.StatusCode = (int)statusCode;
                                    return Task.CompletedTask;
                                }

                                return existingRedirector(context);
                            };
    }
}
