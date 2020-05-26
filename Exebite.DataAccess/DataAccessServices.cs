using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.DataAccess
{
    public static class DataAccessServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection collection)
        {
            collection.AddTransient<ICustomerQueryRepository, CustomerQueryRepository>();
            collection.AddTransient<ICustomerCommandRepository, CustomerCommandRepository>();
            collection.AddTransient<ILocationCommandRepository, LocationCommandRepository>();
            collection.AddTransient<ILocationQueryRepository, LocationQueryRepository>();
            collection.AddTransient<IRestaurantQueryRepository, RestaurantQueryRepository>();
            collection.AddTransient<IRestaurantCommandRepository, RestaurantCommandRepository>();
            collection.AddTransient<IDailyMenuQueryRepository, DailyMenuQueryRepository>();
            collection.AddTransient<IDailyMenuCommandRepository, DailyMenuCommandRepository>();
            collection.AddTransient<IMealOrderingContextFactory, MealOrderingContextFactory>();
            collection.AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>();
            collection.AddTransient<IFoodQueryRepository, FoodQueryRepository>();
            collection.AddTransient<IFoodCommandRepository, FoodCommandRepository>();

            collection.AddTransient<IOrderQueryRepository, OrderQueryRepository>();
            collection.AddTransient<IOrderCommandRepository, OrderCommandRepository>();
            collection.AddTransient<IMealQueryRepository, MealQueryRepository>();
            collection.AddTransient<IMealCommandRepository, MealCommandRepository>();

            return collection;
        }
    }
}
