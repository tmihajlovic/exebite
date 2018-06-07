using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Exebite.DataAccess.Migrations
{
    public class FoodOrderingContext : DbContext, IFoodOrderingContext
    {
        private readonly DbContextOptions<FoodOrderingContext> _dbContextOptions;

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

        public DbSet<FoodEntityRecipeEntity> FoodEntityRecipeEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodEntity>()
                .HasOne(r => r.Restaurant)
                .WithMany(f => f.Foods)
                .HasForeignKey(k => k.RestaurantId);

            modelBuilder.Entity<FoodEntityMealEntities>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<CustomerAliasesEntities>()
                .HasOne(c => c.Customer)
                .WithMany(a => a.Aliases);

            modelBuilder.Entity<FoodEntityRecipeEntity>()
                .HasKey(k => new { k.FoodEntityId, k.RecepieEntityId });

            modelBuilder.Entity<FoodEntityRecipeEntity>()
                .HasOne(r => r.RecipeEntity)
                .WithMany(fr => fr.FoodEntityRecipeEntities)
                .HasForeignKey(k => k.RecepieEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantEntity>()
                .HasMany(f => f.DailyMenu);

            modelBuilder.Entity<FoodEntityRecipeEntity>()
                .HasKey(k => new { k.FoodEntityId, k.RecepieEntityId });
        }
    }
}
