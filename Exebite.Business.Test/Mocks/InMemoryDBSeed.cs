using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Migrations;

namespace Exebite.Business.Test.Mocks
{
    public static class InMemoryDBSeed
    {
        public static void Seed(IFoodOrderingContextFactory contextFactory)
        {
            var context = contextFactory.Create();

            var isSeeded = context.Restaurants.Any();
            if (isSeeded)
            {
                return;
            }

            // Seed restaurants
            context.Restaurants.Add(new DataAccess.Entities.RestaurantEntity { Name = "Restoran pod Lipom" });
            context.Restaurants.Add(new DataAccess.Entities.RestaurantEntity { Name = "Hedone" });
            context.Restaurants.Add(new DataAccess.Entities.RestaurantEntity { Name = "Index House" });
            context.Restaurants.Add(new DataAccess.Entities.RestaurantEntity { Name = "Teglas" });
            context.Restaurants.Add(new DataAccess.Entities.RestaurantEntity { Name = "Extra Food" });
            context.Restaurants.Add(new DataAccess.Entities.RestaurantEntity { Name = "For delete" });
            context.SaveChanges();

            // Seed locations
            context.Locations.Add(new DataAccess.Entities.LocationEntity { Name = "Bulevar", Address = "Bulevar Vojvode Stjepe 50" });
            context.Locations.Add(new DataAccess.Entities.LocationEntity { Name = "JD", Address = "Jovana Ducica" });
            context.Locations.Add(new DataAccess.Entities.LocationEntity { Name = "For delete", Address = "For delete" });
            context.SaveChanges();

            // Seed customer
            context.Customers.Add(new DataAccess.Entities.CustomerEntity
            {
                Name = "Test Customer",
                AppUserId = "TestAppUserId",
                Location = context.Locations.Single(l => l.Id == 1),
                Balance = 0,
                LocationId = 1
            });
            context.Customers.Add(new DataAccess.Entities.CustomerEntity
            {
                Name = "Test Customer for delete",
                AppUserId = "TestAppUserId2",
                Location = context.Locations.Single(l => l.Id == 1),
                Balance = 0,
                LocationId = 1
            });
            context.SaveChanges();

            // Seed alias
            context.CustomerAliases.Add(new DataAccess.Entities.CustomerAliasesEntities
            {
                Alias = "Test customer alias",
                Customer = context.Customers.Find(1),
                CustomerId = 1,
                Restaurant = context.Restaurants.Find(1),
                RestaurantId = 1
            });
            context.SaveChanges();

            // Seed food
            context.Foods.Add(new DataAccess.Entities.FoodEntity
            {
                Name = "Test food",
                Description = "Test food description",
                IsInactive = false,
                Price = 100,
                Type = Model.FoodType.MAIN_COURSE,
                RestaurantId = 1,
                Restaurant = context.Restaurants.Find(1)
            });

            context.Foods.Add(new DataAccess.Entities.FoodEntity
            {
                Name = "Test food for delete",
                Description = "Food for delete",
                IsInactive = false,
                Price = 100,
                Type = Model.FoodType.MAIN_COURSE,
                RestaurantId = 1,
                Restaurant = context.Restaurants.Find(1)
            });

            context.Foods.Add(new DataAccess.Entities.FoodEntity
            {
                Name = "Test side dish",
                Description = "Side dish",
                IsInactive = false,
                Price = 100,
                Type = Model.FoodType.SIDE_DISH,
                RestaurantId = 1,
                Restaurant = context.Restaurants.Find(1)
            });

            context.Foods.Add(new DataAccess.Entities.FoodEntity
            {
                Name = "Test condament 1",
                Description = "Condament",
                IsInactive = false,
                Price = 100,
                Type = Model.FoodType.CONDIMENTS,
                RestaurantId = 1,
                Restaurant = context.Restaurants.Find(1)
            });

            context.Foods.Add(new DataAccess.Entities.FoodEntity
            {
                Name = "Test salad",
                Description = "Salad",
                IsInactive = false,
                Price = 100,
                Type = Model.FoodType.SALAD,
                RestaurantId = 1,
                Restaurant = context.Restaurants.Find(1)
            });

            context.SaveChanges();

            // Seed orders
            var tmp = context.Orders.Add(new DataAccess.Entities.OrderEntity
            {
                CustomerId = 1,
                Customer = context.Customers.Find(1),
                Date = DateTime.Today,
                Note = "Test Note",
                Price = 100,
                Meal = new DataAccess.Entities.MealEntity()
            });
            tmp.Entity.MealId = tmp.Entity.Meal.Id;
            tmp.Entity.Meal.Price = 100;
            tmp.Entity.Meal.FoodEntityMealEntities = new List<DataAccess.Entities.FoodEntityMealEntities>()
            {
                new DataAccess.Entities.FoodEntityMealEntities()
                {
                    FoodEntityId = 1,
                    FoodEntity = context.Foods.Find(1),
                    MealEntity = tmp.Entity.Meal,
                    MealEntityId = tmp.Entity.Meal.Id
                }
            };

            context.SaveChanges();

            var tmp2 = context.Orders.Add(new DataAccess.Entities.OrderEntity
            {
                CustomerId = 1,
                Customer = context.Customers.Find(1),
                Date = DateTime.Today,
                Note = "For delete",
                Price = 100,
                Meal = new DataAccess.Entities.MealEntity()
            });
            tmp2.Entity.MealId = tmp2.Entity.Meal.Id;
            tmp2.Entity.Meal.Price = 100;
            tmp2.Entity.Meal.FoodEntityMealEntities = new List<DataAccess.Entities.FoodEntityMealEntities>()
            {
                new DataAccess.Entities.FoodEntityMealEntities()
                {
                    FoodEntityId = 1,
                    FoodEntity = context.Foods.Find(1),
                    MealEntity = tmp2.Entity.Meal,
                    MealEntityId = tmp2.Entity.Meal.Id
                }
            };
            context.SaveChanges();

            // Seed recipes
            var recipe1 = context.Recipes.Add(new DataAccess.Entities.RecipeEntity
            {
                MainCourse = context.Foods.Find(1),
                MainCourseId = 1,
                Restaurant = context.Restaurants.Find(1),
                RestaurantId = 1
            });
            recipe1.Entity.FoodEntityRecipeEntities = new List<DataAccess.Entities.FoodEntityRecipeEntity>
            {
                new DataAccess.Entities.FoodEntityRecipeEntity
                {
                     FoodEntity = context.Foods.FirstOrDefault(f => f.Type == Model.FoodType.CONDIMENTS),
                     FoodEntityId = context.Foods.FirstOrDefault(f => f.Type == Model.FoodType.CONDIMENTS).Id,
                     RecipeEntity = recipe1.Entity,
                     RecepieEntityId = recipe1.Entity.Id
                }
            };
            context.SaveChanges();

            var recipe2 = context.Recipes.Add(new DataAccess.Entities.RecipeEntity
            {
                MainCourse = context.Foods.Find(3),
                MainCourseId = 3,
                Restaurant = context.Restaurants.Find(1),
                RestaurantId = 1
            });
            recipe2.Entity.FoodEntityRecipeEntities = new List<DataAccess.Entities.FoodEntityRecipeEntity>
            {
                new DataAccess.Entities.FoodEntityRecipeEntity
                {
                     FoodEntity = context.Foods.FirstOrDefault(f => f.Type == Model.FoodType.SIDE_DISH),
                     FoodEntityId = context.Foods.FirstOrDefault(f => f.Type == Model.FoodType.SIDE_DISH).Id,
                     RecipeEntity = recipe2.Entity,
                     RecepieEntityId = recipe2.Entity.Id
                }
            };
            context.SaveChanges();

            // Add foods to daily menu
            var restaurant = context.Restaurants.Find(1);
            restaurant.DailyMenu = new List<DataAccess.Entities.FoodEntity>();
            restaurant.DailyMenu.AddRange(restaurant.Foods);
            context.SaveChanges();
        }
    }
}
