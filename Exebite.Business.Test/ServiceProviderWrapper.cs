using System;
using AutoMapper;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.Business.Test
{
    public static class ServiceProviderWrapper
    {
        public static IServiceProvider GetContainer()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;

            var serviceProvider = new ServiceCollection()
                                        .AddLogging()
                                        .AddTransient<IFoodOrderingContextFactory, InMemoryDBFactory>()
                                        .AddTransient<IRestaurantRepository, RestaurantRepository>()
                                        .AddTransient<IFoodRepository, FoodRepository>()
                                        .AddTransient<IRecipeRepository, RecipeRepository>()
                                        .AddTransient<ICustomerRepository, CustomerRepository>()
                                        .AddTransient<ILocationRepository, LocationRepository>()
                                        .AddTransient<IOrderRepository, OrderRepository>()

                                        .AddTransient<IFoodRepository, FoodRepository>()
                                        .AddTransient<IMenuService, MenuService>()
                                        .AddTransient<IRestaurantRepository, RestaurantRepository>()
                                        .AddTransient<ICustomerRepository, CustomerRepository>()
                                        .AddTransient<ILocationRepository, LocationRepository>()
                                        .AddTransient<IOrderService, OrderService>()

                                        .AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>()

                                        .AddTransient<IMapper, Mapper>()
                                        .AddAutoMapper(cfg => cfg.AddProfile<DataAccessMappingProfile>())
                                        .AddTransient<IFoodToFoodEntityConverter, FoodToFoodEntityConverter>()
                                        .AddTransient<IMealToMealEntityConverter, MealToMealEntityConverter>()
                                        .AddTransient<IRecipeToRecipeEntityConverter, RecipeToRecipeEntityConverter>()

                                        .BuildServiceProvider();
            return serviceProvider;
        }

        public static T Resolve<T>(this IServiceProvider provider)
        {
            var res = (T)provider.GetService(typeof(T));
            return res;
        }
    }
}
