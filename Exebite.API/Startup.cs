using System;
using System.Reflection;
using AutoMapper;
using Exebite.API.Authorization;
using Exebite.Business;
using Exebite.DataAccess;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;

namespace Exebite.API
{
    public class Startup
    {
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env, IServiceProvider provider)
        {
            _configuration = configuration;
            _hostingEnvironment = env;
            _provider = provider;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = GoogleDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = _configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = _configuration["Authentication:Google:ClientSecret"];
                });

            if (_hostingEnvironment.IsDevelopment())
            {
                services.AddMvc(opts => opts.Filters.Add(new AllowAnonymousFilter()));
            }
            else
            {
                services.AddMvc();
            }

            // when we get client id and secret uncomment this
            //services.AddAuthorization(options => options.AddCustomPolicies());

            services.AddTransient<IRoleService, RoleService>();

            services.AddAutoMapper(
                cfg =>
                {
                    cfg.ConstructServicesUsing(x => _provider.GetService(x));
                    cfg.AddProfile<DataAccessMappingProfile>();
                    cfg.AddProfile<UIMappingProfile>();
                })
            .AddDataAccessServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStatusCodePages();

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
        }
    }
}
