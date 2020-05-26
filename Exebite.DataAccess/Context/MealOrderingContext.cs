using System;
using System.Linq;
using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Context
{
    public class MealOrderingContext : DbContext, IMealOrderingContext
    {
        private readonly string _lastModified = "LastModified";
        private readonly string _created = "Created";

        private readonly DbContextOptions<MealOrderingContext> _dbContextOptions;

        public MealOrderingContext(DbContextOptions<MealOrderingContext> dbContextOptions)
            : base(dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public DbSet<OrderEntity> Order { get; set; }

        public DbSet<CustomerEntity> Customer { get; set; }

        public DbSet<MealEntity> Meal { get; set; }

        public DbSet<RestaurantEntity> Restaurant { get; set; }

        public DbSet<LocationEntity> Location { get; set; }

        public DbSet<DailyMenuEntity> DailyMenu { get; set; }

        public DbSet<PaymentEntity> Payment { get; set; }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries()
                                              .Where(e => e.State == EntityState.Added
                                                       || e.State == EntityState.Modified))
            {
                entry.Property(_lastModified).CurrentValue = timestamp;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(_created).CurrentValue = timestamp;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyMenuToMealEntity>()
                .HasKey(k => new { k.DailyMenuId, k.MealId });

            modelBuilder.Entity<MealToCondimentEntity>()
                .HasKey(k => new { k.MealId, k.CondimentId });

            modelBuilder.Entity<CustomerToFavouriteMealEntity>()
                .HasKey(k => new { k.CustomerId, k.MealId });

            modelBuilder.Entity<RestaurantEntity>()
                .HasIndex(x => x.SheetId);

            modelBuilder.Entity<DailyMenuEntity>()
                .HasOne(x => x.Restaurant);

            modelBuilder.Entity<PaymentEntity>()
                .HasOne(x => x.Customer);

            modelBuilder.Entity<OrderEntity>()
                .HasIndex(x => x.Date);

            modelBuilder.Entity<CustomerEntity>()
                .HasIndex(x => x.GoogleUserId);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>(_created);
                modelBuilder.Entity(entityType.Name).Property<DateTime>(_lastModified);
            }
        }
    }
}
