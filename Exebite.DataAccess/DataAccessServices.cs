using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.DataAccess
{
    public static class DataAccessServices
    {

        public static IServiceCollection AddDataAccessServices(this IServiceCollection collection)
        {
            collection.AddTransient<ICustomerRepository, CustomerRepository>();
            collection.AddTransient<ILocationRepository, LocationRepository>();
            collection.AddTransient<IRestaurantRepository, RestaurantRepository>();
            collection.AddTransient<IFoodOrderingContextFactory, FoodOrderingContextFactory>();
            collection.AddTransient<IExebiteDbContextOptionsFactory, ExebiteDbContextOptionsFactory>();
            collection.AddTransient<IExebiteMapper, ExebiteMapper>();
            return collection;
        }
    }
}
