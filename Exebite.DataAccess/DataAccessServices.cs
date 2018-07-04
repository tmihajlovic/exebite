using Exebite.DataAccess.AutoMapper;
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
            collection.AddTransient<IFoodOrderingContextFactory, FoodOrderingContextFactory>();
            collection.AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>();
            collection.AddTransient<IFoodRepository, FoodRepository>();
            collection.AddTransient<IRecipeRepository, RecipeRepository>();
            collection.AddTransient<IOrderRepository, OrderRepository>();
            collection.AddTransient<IMealRepository, MealRepository>();
            collection.AddTransient<IRecipeToRecipeEntityConverter, RecipeToRecipeEntityConverter>();
            collection.AddTransient<IFoodToFoodEntityConverter, FoodToFoodEntityConverter>();
            return collection;
        }
    }
}
