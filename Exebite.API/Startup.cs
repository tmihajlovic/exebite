using System;
using AutoMapper;
using Exebite.Business;
using Exebite.DataAccess;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Exebite.DataAccess;

namespace Exebite.API
{
    public class Startup
    {
        private readonly IServiceProvider provider;

        public Startup(IConfiguration configuration, IHostingEnvironment env, IServiceProvider provider)
        {
            Configuration = configuration;
            HostingEnvironment = env;
            this.provider = provider;
        }

        public static IConfiguration Configuration { get; private set; }

        public static IHostingEnvironment HostingEnvironment { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle();

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddMvc(opts => opts.Filters.Add(new AllowAnonymousFilter()));
            }
            else
            {
                services.AddMvc();
            }

            services.AddAutoMapper(
                cfg =>
                {
                    cfg.ConstructServicesUsing(x => this.provider.GetService(x));
                    cfg.AddProfile<DataAccessMappingProfile>();
                    cfg.AddProfile<UIMappingProfile>();
                    if (HostingEnvironment.IsDevelopment())
                    {
                        Mapper.Configuration.AssertConfigurationIsValid();
                    }
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
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();
        }
    }
}
