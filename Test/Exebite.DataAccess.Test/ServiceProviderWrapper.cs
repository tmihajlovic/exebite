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
            Mapper.Reset();
            var serviceProvider = new ServiceCollection()
                                        .AddLogging()
                                        .AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>()
                                        .AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>()
                                        .AddTransient<IFoodOrderingContextFactory, InMemoryDBFactory>()
                                        .AddTransient<IRestaurantCommandRepository, RestaurantCommandRepository>()
                                        .AddTransient<IRestaurantQueryRepository, RestaurantQueryRepository>()
                                        .AddTransient<IFoodQueryRepository, FoodQueryRepository>()
                                        .AddTransient<IFoodCommandRepository, FoodCommandRepository>()
                                        .AddTransient<IRecipeQueryRepository, RecipeQueryRepository>()
                                        .AddTransient<IRecipeCommandRepository, RecipeCommandRepository>()
                                        .AddTransient<ICustomerQueryRepository, CustomerQueryRepository>()
                                        .AddTransient<ICustomerCommandRepository, CustomerCommandRepository>()
                                        .AddTransient<ILocationCommandRepository, LocationCommandRepository>()
                                        .AddTransient<ILocationQueryRepository, LocationQueryRepository>()
                                        .AddTransient<IOrderCommandRepository, OrderCommandRepository>()
                                        .AddTransient<IOrderQueryRepository, OrderQueryRepository>()
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
