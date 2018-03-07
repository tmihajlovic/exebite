using Exebite.DataAccess.Entities;
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
        public DbSet<CustomerAliasesEntity> CustomerAliases { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantEntity>()
               .HasMany(f => f.Foods);

            modelBuilder.Entity<FoodEntity>()
                .HasRequired(r => r.Restaurant)
                .WithMany(f => f.Foods);

            
        }
    }
}
