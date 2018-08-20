using Microsoft.Extensions.DependencyInjection;
using WebClient.Services;

namespace WebClient.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRestServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILocationService, LocationService>();
            serviceCollection.AddTransient<IFoodService, FoodService>();
            serviceCollection.AddTransient<ICustomerAliasService, CustormerAliasService>();
            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            serviceCollection.AddTransient<IDailyMenuService, DailyMenuService>();
        }
    }
}
