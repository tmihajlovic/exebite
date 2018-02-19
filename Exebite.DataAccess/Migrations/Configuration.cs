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
                new Locations.LocationEntity() { Id = 1, Name = "Bulevar", Address = "Bulevar Vojvode Stjepe" },
                new Locations.LocationEntity() { Id = 2, Name = "JD", Address = "Jovana Ducica" }
                 );
        }
    }
}
