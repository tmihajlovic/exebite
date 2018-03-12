using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Unity;

namespace Exebite.DataAccess.Unity
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IFoodOrderingContextFactory, FoodOrderingContextFactory>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IFoodRepository, FoodRepository>();
            container.RegisterType<ILocationRepository, LocationRepository>();
            container.RegisterType<IMealRepository, MealRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IRecipeRepository, RecipeRepository>();
            container.RegisterType<IRestaurantRepository, RestaurantRepository>();
        }
    }
}
