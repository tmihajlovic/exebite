using Exebite.IdentityServer.Interfaces;
using Exebite.IdentityServer.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Exebite.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _corsPolicy = "corsPolicy";

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _hostingEnvironment = env;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.EmitStaticAudienceClaim = true;
            })
                .AddProfileService<ProfileService>();

            builder.AddInMemoryIdentityResources(Config.IdentityResources);
            builder.AddInMemoryClients(Config.Clients);

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = _configuration["Apps:Google:ClientId"];
                    options.ClientSecret = _configuration["Apps:Google:ClientSecret"];
                    options.ClaimActions.MapAll();
                });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    _corsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin() // TODO - Before app is deployed to production, add only necessary origins
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddHttpClient<IExebiteApiService, ExebiteApiService>(c =>
            {
                c.BaseAddress = new Uri(_configuration["Apps:Exebite.API:Url"]);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_corsPolicy);

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
