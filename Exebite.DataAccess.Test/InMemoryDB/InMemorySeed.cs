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
            context.Restaurants.Add(new Entities.RestaurantEntity { Name = "Restoran pod Lipom" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Name = "Hedone" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Name = "Index House" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Name = "Teglas" });
            context.Restaurants.Add(new Entities.RestaurantEntity { Name = "Extra Food" });
            context.SaveChanges();

            // Seed locations
            context.Locations.Add(new Entities.LocationEntity { Name = "Bulevar", Address = "Bulevar Vojvode Stjepe 50" });
            context.Locations.Add(new Entities.LocationEntity { Name = "JD", Address = "Jovana Ducica" });
            context.Locations.Add(new Entities.LocationEntity { Name = "For delete", Address = "For delete" });
            context.SaveChanges();

            // Seed customer
            context.Customers.Add(new Entities.CustomerEntity
            {
                Name = "Test Customer",
                AppUserId = "TestAppUserId",
                Location = context.Locations.Single(l => l.Id == 1), Balance = 0,
                LocationId = 1
            });
            context.Customers.Add(new Entities.CustomerEntity
            {
                Name = "Test Customer for delete",
                AppUserId = "TestAppUserId",
                Location = context.Locations.Single(l => l.Id == 1),
                Balance = 0,
                LocationId = 1
            });
            context.SaveChanges();

            // Seed food
            context.Foods.Add(new Entities.FoodEntity
            {
                Name = "Test food",
                Description = "Test food description",
                IsInactive = false, Price = 100,
                Type = Model.FoodType.MAIN_COURSE,
                RestaurantId = 1,
                Restaurant = context.Restaurants.Find(1)
            });
            context.Foods.Add(new Entities.FoodEntity
            {
                Name = "Test food for delete",
                Description = "Food for delete",
                IsInactive = false, Price = 100,
                Type = Model.FoodType.MAIN_COURSE,
                RestaurantId = 1,
                Restaurant = context.Restaurants.Find(1)
            });
            context.SaveChanges();

            // Seed orders
            var tmp = context.Orders.Add(new Entities.OrderEntity
            {
                CustomerId = 1,
                Customer = context.Customers.Find(1),
                Date = DateTime.Today,
                Note = "Test Note",
                Price = 100,
                Meal = new Entities.MealEntity()
            });
            tmp.Entity.MealId = tmp.Entity.Meal.Id;
            tmp.Entity.Meal.Price = 100;
            tmp.Entity.Meal.FoodEntityMealEntities = new List<Entities.FoodEntityMealEntities>()
            {
                new Entities.FoodEntityMealEntities()
                {
                    FoodEntityId = 1,
                    FoodEntity = context.Foods.Find(1),
                    MealEntity = tmp.Entity.Meal,
                    MealEntityId = tmp.Entity.Meal.Id
                }
            };

            context.SaveChanges();

            var tmp2 = context.Orders.Add(new Entities.OrderEntity
            {
                CustomerId = 1,
                Customer = context.Customers.Find(1),
                Date = DateTime.Today,
                Note = "For delete",
                Price = 100,
                Meal = new Entities.MealEntity()
            });
            tmp2.Entity.MealId = tmp2.Entity.Meal.Id;
            tmp2.Entity.Meal.Price = 100;
            tmp2.Entity.Meal.FoodEntityMealEntities = new List<Entities.FoodEntityMealEntities>()
            {
                new Entities.FoodEntityMealEntities()
                {
                    FoodEntityId = 1,
                    FoodEntity = context.Foods.Find(1),
                    MealEntity = tmp2.Entity.Meal,
                    MealEntityId = tmp2.Entity.Meal.Id
                }
            };
            context.SaveChanges();
        }
    }
}
