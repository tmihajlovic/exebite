using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AutoMapper;
using Exebite.API.Authorization;
using Exebite.API.Extensions;
using Exebite.Business;
using Exebite.Common;
using Exebite.DataAccess;
using Exebite.GoogleSheetAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag.AspNetCore;

namespace Exebite.API
{
    public class Startup
    {
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _myAllowSpecificOrigins = "myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment env, IServiceProvider provider)
        {
            _configuration = configuration;
            _hostingEnvironment = env;
            _provider = provider;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied = Helper.ReplaceRedirector(HttpStatusCode.Forbidden, options.Events.OnRedirectToAccessDenied);
                options.Events.OnRedirectToLogin = Helper.ReplaceRedirector(HttpStatusCode.Unauthorized, options.Events.OnRedirectToLogin);
            });

            if (!_hostingEnvironment.IsDevelopment())
            {
                services.AddMvc(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
                    opts.EnableEndpointRouting = false;
                })
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNSwagSettings(); // Add NSwag CamelCase settings.

                services.AddCors(options =>
                {
                    options.AddPolicy(
                        _myAllowSpecificOrigins,
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                        });
                });
            }
            else
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = _configuration["Apps:Exebite.IdentityServer:Url"];

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidAudiences = new List<string>
                            {
                                _configuration["Apps:Exebite.API:Name"],
                                $"{_configuration["Apps:Exebite.IdentityServer:Url"]}/resources"
                            },
                            ValidTypes = new[] { "at+jwt" }
                        };
                    });

                services.AddMvc(opts =>
                {
                    opts.EnableEndpointRouting = false;
                })
                .AddNSwagSettings(); // Add NSwag CamelCase settings.

                services.AddCors(options =>
                {
                    options.AddPolicy(
                        _myAllowSpecificOrigins,
                        builder =>
                        {
                            builder.AllowAnyOrigin() // TODO - Change later for production
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                        });
                });
            }

            services.AddAuthorization(options => options.AddCustomPolicies());
            services.AddIdentityCore<IdentityUser>();
            services.AddScoped<IAuthorizationHandler, RoleHandler>();
            services.AddTransient<IAuthorizationHandler, RoleHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddTransient<IAuthorizationHandler, PermissionHandler>();

            services.AddAutoMapper(
                cfg =>
                {
                    cfg.ConstructServicesUsing(x => _provider.GetService(x));
                    cfg.AddProfile<DataAccessMappingProfile>();
                    cfg.AddProfile<UIMappingProfile>();
                })

            .AddDataAccessServices() // Exebite.DataAccess services
            .AddCommonServices() // Exebite.Common services
            .AddBusinessServices() // Exebite.Business services
            .AddGoogleSheetApiServices(); // Exebite.GoogleSheetAPI services

            services.Configure<IISOptions>(x =>
            {
                x.ForwardClientCertificate = false;
            });
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRewriter(new RewriteOptions()
                .AddRedirect("swager", "swagger")
                .AddRedirect("apiDefinition", "swagger"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors(_myAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStatusCodePages();

            app.UseMvc();

            // Nswag3 with updated UI.
            app.UseSwagger();
            app.UseSwaggerUi3();
        }
    }
}
