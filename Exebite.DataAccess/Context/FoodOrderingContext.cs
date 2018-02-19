using Exebite.DataAccess.Customers;
using Exebite.DataAccess.Foods;
using Exebite.DataAccess.Locations;
using Exebite.DataAccess.Meals;
using Exebite.DataAccess.Orders;
using Exebite.DataAccess.Recipes;
using Exebite.DataAccess.Restaurants;
using System.Data.Entity;
namespace Exebite.DataAccess.Context
{
    public class FoodOrderingContext : DbContext
    {
        public DbSet<FoodEntity> Foods{ get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<MealEntity> Meals { get; set; }
        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
    }
}
