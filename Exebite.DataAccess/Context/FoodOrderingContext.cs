using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Context
{
    public class FoodOrderingContext : DbContext
    {
        private DbContextOptions<FoodOrderingContext> _dbContextOptions;

        public FoodOrderingContext(DbContextOptions<FoodOrderingContext> dbContextOptions)
            : base(dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public DbSet<FoodEntity> Foods { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<MealEntity> Meals { get; set; }

        public DbSet<RestaurantEntity> Restaurants { get; set; }

        public DbSet<RecipeEntity> Recipes { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }

        public DbSet<CustomerAliasesEntities> CustomerAliases { get; set; }

        public DbSet<FoodEntityMealEntities> FoodEntityMealEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodEntity>()
                .HasOne(r => r.Restaurant)
                .WithMany(f => f.Foods);

            modelBuilder.Entity<FoodEntity>()
                .HasMany(r => r.Recipes);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(f => f.Foods);

            modelBuilder.Entity<RecipeEntity>()
                .HasOne(f => f.MainCourse);

            modelBuilder.Entity<FoodEntityMealEntities>()
                .HasKey(t => new { t.FoodEntityId, t.MealEntityId });

            modelBuilder.Entity<CustomerAliasesEntities>()
                .HasOne(c => c.Customer)
                .WithMany(a => a.Aliases);
        }
    }
}
