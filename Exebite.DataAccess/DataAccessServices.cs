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
            collection.AddTransient<IFoodQueryRepository, FoodQueryRepository>();
            collection.AddTransient<IFoodCommandRepository, FoodCommandRepository>();
            collection.AddTransient<ICustomerAliasQueryRepository, CustomerAliasQueryRepository>();
            collection.AddTransient<ICustomerAliasCommandRepository, CustomerAliasCommandRepository>();

            collection.AddTransient<IRecipeQueryRepository, RecipeQueryRepository>();
            collection.AddTransient<IRecipeCommandRepository, RecipeCommandRepository>();

            collection.AddTransient<IOrderQueryRepository, OrderQueryRepository>();
            collection.AddTransient<IOrderCommandRepository, OrderCommandRepository>();
            collection.AddTransient<IMealQueryRepository, MealQueryRepository>();
            collection.AddTransient<IMealCommandRepository, MealCommandRepository>();
            collection.AddTransient<IRecipeToRecipeEntityConverter, RecipeToRecipeEntityConverter>();
            collection.AddTransient<IFoodToFoodEntityConverter, FoodToFoodEntityConverter>();
            collection.AddTransient<IRoleQueryRepository, RoleQueryRepository>();
            collection.AddTransient<IRoleCommandRepository, RoleCommandRepository>();

            return collection;
        }
    }
}
