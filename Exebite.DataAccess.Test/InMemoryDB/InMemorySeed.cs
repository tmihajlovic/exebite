using Exebite.DataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exebite.DataAccess.Test.InMemoryDB
{
    public static class InMemorySeed
    {

        public static void Seed(IFoodOrderingContextFactory contextFactory)
        {
            var context = contextFactory.Create();

            // Seed restaurants
            context.Restaurants.Add(new Entities.RestaurantEntity { Id = 1, Name = "Restoran pod Lipom" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Id = 2, Name = "Hedone" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Id = 3, Name = "Index House" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Id = 4, Name = "Teglas" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Id = 5, Name = "Extra Food" });
            context.SaveChanges();

            // Seed locations
            context.Locations.Add(new Entities.LocationEntity { Id = 1, Name = "Bulevar", Address = "Bulevar Vojvode Stjepe 50" });
            context.Locations.Add(new Entities.LocationEntity { Id = 2, Name = "JD", Address = "Jovana Ducica" });
            context.SaveChanges();

            // Seed customer
            context.Customers.Add(new Entities.CustomerEntity { Name = "Test Customer", AppUserId = "TestAppUserId", Location = context.Locations.Single(l => l.Id == 1), Balance = 0, LocationId = 1 });
            context.SaveChanges();
        }
    }
}
