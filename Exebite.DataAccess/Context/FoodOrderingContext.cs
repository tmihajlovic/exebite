using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Context
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

        public DbSet<DailyMenuEntity> DailyMenues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodEntity>()
                .HasOne(r => r.Restaurant)
                .WithMany(f => f.Foods)
                .HasForeignKey(k => k.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodEntityMealEntities>()
                .HasKey(k => new { k.FoodEntityId, k.MealEntityId });

            modelBuilder.Entity<CustomerAliasesEntities>()
                .HasOne(c => c.Customer)
                .WithMany(a => a.Aliases);

            modelBuilder.Entity<FoodEntityRecipeEntity>()
                .HasKey(k => new { k.FoodEntityId, k.RecepieEntityId });

            modelBuilder.Entity<FoodEntityRecipeEntity>()
                .HasOne(r => r.RecipeEntity)
                .WithMany(fr => fr.FoodEntityRecipeEntities)
                .HasForeignKey(k => k.RecepieEntityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodEntityRecipeEntity>()
                .HasOne(r => r.FoodEntity)
                .WithMany(fr => fr.FoodEntityRecipeEntities)
                .HasForeignKey(k => k.FoodEntityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RestaurantEntity>()
                .HasIndex(x => x.Name);

            modelBuilder.Entity<DailyMenuEntity>()
                .HasOne(x => x.Restaurant);

            modelBuilder.Entity<DailyMenuEntity>()
                .HasMany(x => x.Menu);
        }
    }
}
