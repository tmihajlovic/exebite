using System;
using AutoMapper;
using Exebite.DataAccess;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.Test
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

                                        .AddTransient<IRecipeToRecipeEntityConverter, RecipeToRecipeEntityConverter>()
                                        .AddTransient<IFoodToFoodEntityConverter, FoodToFoodEntityConverter>()
                                        .AddTransient<IMealToMealEntityConverter, MealToMealEntityConverter>()
                                        .AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>()
                                        .AddAutoMapper(cfg =>
                                                    cfg.AddProfile<DataAccessMappingProfile>())
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
