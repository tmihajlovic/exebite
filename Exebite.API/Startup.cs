using Exebite.API.Models;
using Exebite.Business;
using Exebite.DataAccess;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IFoodOrderingContextFactory, FoodOrderingContextFactory>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IFoodService, FoodService>();
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
