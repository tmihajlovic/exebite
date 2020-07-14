using System;
using System.Net;
using System.Text;
using AutoMapper;
using Exebite.API.Authorization;
using Exebite.API.Extensions;
using Exebite.Business;
using Exebite.Common;
using Exebite.DataAccess;
using Exebite.GoogleSheetAPI;
using Microsoft.AspNetCore.Authentication.Google;
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
                services
                    /*.AddAuthentication(options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    })
                    .AddCookie()*/
                    .AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false; // TODO - Change later for production
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:Exebite.API:ClientSecret"])),
                            ValidIssuer = _configuration["Authentication:Exebite.API:ClientId"],
                            ValidateIssuer = true,
                            ValidAudience = _configuration["Authentication:Exebite.ClientApp:ClientId"],
                            ValidateAudience = true,
                        };
                    })
                    .AddGoogle(googleOptions =>
                    {
                        googleOptions.ClientId = _configuration["Authentication:Exebite.API:ClientId"];
                        googleOptions.ClientSecret = _configuration["Authentication:Exebite.API:ClientSecret"];
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
                            builder.AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                        });
                });
            }

            services.AddAuthorization(options => options.AddCustomPolicies());
            services.AddIdentityCore<IdentityUser>();
            services.AddScoped<IAuthorizationHandler, RoleHandler>();
            services.AddTransient<IAuthorizationHandler, RoleHandler>();

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
