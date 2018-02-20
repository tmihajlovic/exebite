namespace Exebite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Exebite.DataAccess.Context.FoodOrderingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Exebite.DataAccess.Context.FoodOrderingContext";
        }

        protected override void Seed(Exebite.DataAccess.Context.FoodOrderingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Locations.AddOrUpdate(x => x.Id,
                new Handlers.LocationEntity() { Id = 1, Name = "Bulevar", Address = "Bulevar Vojvode Stjepe" },
                new Handlers.LocationEntity() { Id = 2, Name = "JD", Address = "Jovana Ducica" }
                 );
            context.Restaurants.AddOrUpdate(x => x.Id,
                new Handlers.RestaurantEntity() { Id = 1, Name = "Restoran pod Lipom" },
                new Handlers.RestaurantEntity() { Id = 2, Name = "Hedone" },
                new Handlers.RestaurantEntity() { Id = 3, Name = "Index House" },
                new Handlers.RestaurantEntity() { Id = 4, Name = "Teglas" },
                new Handlers.RestaurantEntity() { Id = 5, Name = "Extra Food" }
                );
        }
    }
}
