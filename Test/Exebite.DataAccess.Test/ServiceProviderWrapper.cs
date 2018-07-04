using System;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.DataAccess.Test
{
    public static class ServiceProviderWrapper
    {
        public static IServiceProvider GetContainer()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            var serviceProvider = new ServiceCollection()
                                        .AddLogging()
                                        .AddTransient<IFoodOrderingContextFactory, InMemoryDBFactory>()
                                        .AddTransient<IRestaurantCommandRepository, RestaurantCommandRepository>()
                                        .AddTransient<IRestaurantQueryRepository, RestaurantQueryRepository>()
                                        .AddTransient<IFoodQueryRepository, FoodQueryRepository>()
                                        .AddTransient<IFoodCommandRepository, FoodCommandRepository>()
                                        .AddTransient<IRecipeRepository, RecipeRepository>()
                                        .AddTransient<ICustomerQueryRepository, CustomerQueryRepository>()
                                        .AddTransient<ICustomerCommandRepository, CustomerCommandRepository>()
                                        .AddTransient<ILocationCommandRepository, LocationCommandRepository>()
                                        .AddTransient<ILocationQueryRepository, LocationQueryRepository>()
                                        .AddTransient<IOrderRepository, OrderRepository>()
                                        .AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>()
                                        .AddAutoMapper(cfg =>
                                                    cfg.AddProfile<DataAccessMappingProfile>());

            return serviceProvider.BuildServiceProvider();
        }

        public static T Resolve<T>(this IServiceProvider provider)
        {
            var res = (T)provider.GetService(typeof(T));
            return res;
        }
    }
}
