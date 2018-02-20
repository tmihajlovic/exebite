using Exebite.DataAccess.Handlers;
using Exebite.DataAccess.Handlers;
using Exebite.DataAccess.Handlers;
using Exebite.DataAccess.Handlers;
using Exebite.DataAccess.Handlers;
using Exebite.DataAccess.Handlers;
using Exebite.DataAccess.Handlers;
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
